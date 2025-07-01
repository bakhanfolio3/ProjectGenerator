import os
from openai import OpenAI
import ast
import re

client = OpenAI(api_key="ApiKey")

# 🧠 STEP 1: Call GPT to get decisions about which files to create/edit
def get_file_decisions(prompt, folder_structure_text):
    system = (
        "You are an AI code assistant working on a .NET Clean Architecture project."
        " Your ONLY job is to return a Python dictionary named `decisions`."
        " Do NOT include explanation, markdown, or anything except the dictionary."
        " The dictionary tells which files to 'create' or 'need_editing' for the requested feature."
    )

    user_prompt = (
        f"{prompt}\n\n"
        "Below is the folder structure:\n"
        f"{folder_structure_text}\n\n"
        "❗IMPORTANT: STRICTLY return ONLY a Python dictionary named `decisions`, like this:\n"
        "decisions = {\n"
        '    "src/MyNewApp.Application/Features/Order/Query/GetWithDetails/GetOrderWithDetailsQuery.cs": "create",\n'
        '    "src/MyNewApp.Infrastructure/Repositories/OrderRepository.cs": "need_editing"\n'
        "}\n\n"
        "✅ DO NOT return markdown (like ```), explanation, or any unrelated text."
    )

    messages = [
        {"role": "system", "content": system},
        {"role": "user", "content": user_prompt}
    ]

    response = client.chat.completions.create(
        model="gpt-4o",
        messages=messages,
        temperature=0.0
    )

    raw_output = response.choices[0].message.content.strip()

    # 🧼 Clean unwanted markdown/code block formatting if GPT adds any
    # Remove markdown code block if it exists
    cleaned = re.sub(r"^```(?:python)?|```$", "", raw_output.strip(), flags=re.MULTILINE).strip()

    # Extract the dictionary portion from the response
    match = re.search(r"decisions\s*=\s*({.*})", cleaned, re.DOTALL)
    if not match:
        raise ValueError("❌ Could not extract a valid `decisions` dictionary from GPT response.")

    try:
        decisions_dict = ast.literal_eval(match.group(1))
        return decisions_dict
    except Exception as e:
        raise ValueError(f"❌ Failed to parse decisions dictionary: {e}")
    
# ✏️ STEP 2: Call GPT to modify existing file content
def edit_existing_file(prompt, file_content, session_messages):
    session_messages.append({
        "role": "user",
        "content": (
            f"{prompt}\n\n"
            "Update the following C# file content to implement the requested feature.\n"
            "⚠️ Do not include models, DbContext, controller, or unrelated layers.\n"
            "⚠️ Add any missing `using` statements at the top for required namespaces.\n"
            "⚠️ Include all .\n"
            "⚠️ IMPORTANT: Only return the updated C# code. No explanation, no markdown, no wrapping in code blocks.\n\n"
            f"{file_content}"
        )
    })

    response = client.chat.completions.create(
        model="gpt-4o",
        messages=session_messages,
        temperature=0.2
    )

    return response.choices[0].message.content.strip()

# 🆕 STEP 3: Call GPT to create new file content
def generate_new_file_content(prompt, session_messages, target_file_name):
    session_messages.append({
        "role": "user",
        "content": (
            f"You are generating the file `{target_file_name}`.\n"
            f"{prompt}\n\n"
            f"💡 Only generate code for this single file: `{target_file_name}`.\n"
             "⚠️ Do not include models, DbContext, controller, or unrelated layers.\n"
             "⚠️ Add any missing `using` statements at the top for required namespaces.\n"
             "⚠️ ONLY return valid C# code. Do NOT add any markdown formatting or explanation.\n"
        )
    })

    response = client.chat.completions.create(
        model="gpt-4o",
        messages=session_messages,
        temperature=0.2
    )

    raw = response.choices[0].message.content.strip()
    clean = clean_code_output(raw)
    return clean
# 📁 STEP 4: Walk project folder and collect structure (excluding bin/obj etc.)
def extract_dotnet_structure(base_path, exclude_dirs=None):
    if exclude_dirs is None:
        exclude_dirs = {'.git', 'bin', 'obj', '.vs', 'Migrations', '__pycache__'}

    structure = []
    for root, dirs, files in os.walk(base_path):
        dirs[:] = [d for d in dirs if d not in exclude_dirs]
        for file in files:
            if file.endswith('.cs'):
                rel_path = os.path.relpath(os.path.join(root, file), base_path)
                structure.append(rel_path.replace('\\', '/'))
    
    print("structur:{structure}")
    return structure

# 🧾 Format list of paths into GPT-friendly text
def format_structure(paths):
    return "\n".join(paths)

# 🧷 Read content from file
def read_file(path):
    with open(path, 'r', encoding='utf-8') as f:
        return f.read()

# 💾 Write new content to file
def write_file(path, content):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)

def clean_code_output(text):
    # Remove markdown wrappers like ```csharp or ```cs
    return text.strip().removeprefix("```csharp").removeprefix("```cs").removesuffix("```").strip()


# 🧪 Run the full flow (example usage)
if __name__ == "__main__":
    BASE_PATH = "D:\OfficeProjects\ProjectGenerator\MyNewApp"
    PROMPT = "Add support for 'Get Order with OrderDetails' including data access, CQRS handlers, and API layer."

    print("🔍 Extracting folder structure...")
    structure = extract_dotnet_structure(BASE_PATH)
    structure_text = format_structure(structure)

    print("🧠 Sending folder structure to GPT to get decisions...")
    decisions = get_file_decisions(PROMPT, structure_text)

    print("📦 Raw GPT Decision Output:\n", decisions)
    try:
        local_vars = {}
        #exec(decision_text, {}, local_vars)
        #decisions = local_vars.get("decisions")
        if not decisions:
            raise ValueError("GPT did not return a valid `decisions` dictionary.")
    except Exception as e:
        #decisions = local_vars.get("decisions")
        print(decisions)
        print("❌ Error parsing GPT output:", e)
        exit(1)

    session_messages = [{"role": "system", "content": "You are helping with .NET CQRS automation using Clean Architecture."}]

    for rel_path, action in decisions.items():
        abs_path = os.path.join(BASE_PATH, rel_path.replace("/", os.sep).replace("\\", os.sep))
        print(f"stop:{abs_path}")
        if action == "create":
            print(f"🆕 Creating new file: {rel_path}")
            new_content = generate_new_file_content(PROMPT, session_messages,rel_path)
            write_file(abs_path, new_content)

        elif action == "need_editing":
            print(f"✏️ Editing existing file: {rel_path}")
            try:
                existing = read_file(abs_path)
                updated = edit_existing_file(PROMPT, existing, session_messages)
                write_file(abs_path, updated)
            except FileNotFoundError:
                print(f"⚠️ File not found for editing: {rel_path}")
