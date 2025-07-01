import os
import re
import shutil
import subprocess  # Make sure it's at the top


def extract_class_name_from_file(file_path):
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
            match = re.search(r'class\s+([A-Z][A-Za-z0-9_]*)', content)
            return match.group(1) if match else None
    except Exception as e:
        print(f"❌ Could not read model file: {e}")
        return None

def clone_entity_from_project_with_decisions(project_base_path, model_file_path, old_entity_name, decisions):


    new_entity_name = extract_class_name_from_file(model_file_path)
    if not new_entity_name:
        print("❌ Could not extract class name from model file.")
        return

    print(f"🔁 Cloning entity: {old_entity_name} → {new_entity_name}")

    name_map = {
        old_entity_name: new_entity_name,
        old_entity_name.lower(): new_entity_name.lower(),
        old_entity_name.upper(): new_entity_name.upper(),
        old_entity_name[0].lower() + old_entity_name[1:]: new_entity_name[0].lower() + new_entity_name[1:]
    }

    for rel_path, action in decisions.items():
        rel_path = rel_path.replace("\\", "/")
        abs_path = os.path.join(project_base_path, rel_path).replace("\\", "/")

        if action in ["skip"]:
            print(f"⏩ Skipped: {rel_path}")
            continue

        if action == "add_registration":
            try:
                with open(abs_path, 'r+', encoding='utf-8') as f:
                    lines = f.readlines()
                    new_lines = []
                    for line in lines:
                        new_lines.append(line)
                        if old_entity_name in line:
                            new_line = line
                            for old, new in name_map.items():
                                new_line = new_line.replace(old, new)
                            new_lines.append(new_line)
                    f.seek(0)
                    f.writelines(new_lines)
                    f.truncate()
                print(f"✅ Added DI registrations to: {abs_path}")
            except Exception as e:
                print(f"❌ Failed to update DI registrations: {e}")
            continue
        if action == "copy_empty":
            new_abs_path = os.path.join(project_base_path, rel_path.replace(old_entity_name, new_entity_name))
            os.makedirs(new_abs_path, exist_ok=True)
            model_dest = os.path.join(new_abs_path, f"{new_entity_name}.cs")
            shutil.copyfile(model_file_path, model_dest)
            print(f"📁 Created empty entity folder: {new_abs_path}")
            print(f"📄 Model copied to: {model_dest}")
            continue

        if action == "clone_full":
            if os.path.isfile(abs_path):
                # Clone individual file
                try:
                    with open(abs_path, 'r', encoding='utf-8') as f:
                        content = f.read()
                    for old, new in name_map.items():
                        content = content.replace(old, new)
                    new_file_name = os.path.basename(abs_path)
                    for old, new in name_map.items():
                        new_file_name = new_file_name.replace(old, new)
                    new_dir = os.path.dirname(abs_path).replace(old_entity_name, new_entity_name)
                    os.makedirs(new_dir, exist_ok=True)
                    new_file_path = os.path.join(new_dir, new_file_name)
                    with open(new_file_path, 'w', encoding='utf-8') as f:
                        f.write(content)
                    print(f"✅ File cloned: {new_file_path}")
                except Exception as e:
                    print(f"❌ Failed to clone file {abs_path}: {e}")

            elif os.path.isdir(abs_path):
                new_abs_path = abs_path.replace(old_entity_name, new_entity_name)
                try:
                    if not os.path.exists(new_abs_path):
                        shutil.copytree(abs_path, new_abs_path)

                        # Rename filenames inside the copied folder
                        for sub_root, _, sub_files in os.walk(new_abs_path):
                            for file in sub_files:
                                old_file_path = os.path.join(sub_root, file)
                                new_file_name = file
                                for old, new in name_map.items():
                                    new_file_name = new_file_name.replace(old, new)
                                new_file_path = os.path.join(sub_root, new_file_name)
                                if new_file_path != old_file_path:
                                    os.rename(old_file_path, new_file_path)

                        print(f"📁 Folder cloned and renamed files: {abs_path} → {new_abs_path}")
                    else:
                        print(f"⚠️ Folder already exists: {new_abs_path}")

                    # Replace content in files under new path
                    for sub_root, _, sub_files in os.walk(new_abs_path):
                        for file in sub_files:
                            file_path = os.path.join(sub_root, file)
                            try:
                                with open(file_path, 'r', encoding='utf-8') as f:
                                    content = f.read()
                                for old, new in name_map.items():
                                    content = content.replace(old, new)
                                with open(file_path, 'w', encoding='utf-8') as f:
                                    f.write(content)
                            except Exception as e:
                                print(f"❌ Failed to update {file_path}: {e}")
                except Exception as e:
                    print(f"❌ Folder cloning failed {abs_path}: {e}")
        
        if action == "add_migration":
            try:
                migration_name = f"Add{new_entity_name}"
                ef_project_path = os.path.join(project_base_path, "src", "MyNewApp.Infrastructure")
                csproj_path = os.path.join(ef_project_path, "MyNewApp.Infrastructure.csproj")
                db_context_name = "WriteDbContext"  # 🔁 <- REPLACE THIS with your actual DbContext class name

                command = [
                    "dotnet", "ef", "migrations", "add", migration_name,
                    "--project", csproj_path,
                    "--startup-project", csproj_path,
                    "--output-dir", "Migrations",
                    "--context", db_context_name
                ]

                print(f"🏗️ Running EF migration command:\n{' '.join(command)}")
                result = subprocess.run(command, capture_output=True, text=True, encoding='utf-8')

                if result.returncode == 0:
                    print(f"✅ Migration '{migration_name}' added successfully.")
                else:
                    print(f"❌ EF migration failed:\n{result.stdout}\n{result.stderr}")
            except Exception as e:
                print(f"❌ Failed to add migration: {e}")
            continue




decisions = {
    "src\\MyNewApp.Api\\Controllers\\TenantController.cs": "clone_full",
    "src\\MyNewApp.Infrastructure\\CacheKeys\\TenantCacheKeys.cs": "clone_full",
    "src\\MyNewApp.Infrastructure\\CacheRepositories\\TenantCacheRepository.cs": "clone_full",
    "src\\MyNewApp.Infrastructure\\Repositories\\TenantRepository.cs": "clone_full",
    "src\\MyNewApp.Application\\Abstraction\\CacheRepositories\\ITenantCacheRepository.cs": "clone_full",
    "src\\MyNewApp.Application\\Abstraction\\Repositories\\ITenantRepository.cs": "clone_full",
    "src\\MyNewApp.Application\\Abstraction\\Contexts\\IApplicationDbContext.cs": "add_registration",
    "src\\MyNewApp.Infrastructure\\DbContext\\ReadDbContext.cs": "add_registration",
    "src\\MyNewApp.Infrastructure\\DbContext\\WriteDbContext.cs": "add_registration",
    "src\\MyNewApp.Application\\Features\\Tenant": "clone_full",
    "src\\MyNewApp.Domain\\Entities\\Tenant": "copy_empty",
    "src\\MyNewApp.Infrastructure\\Configuration\\Read\\TenantReadConfiguration.cs": "clone_full",
    "src\\MyNewApp.Infrastructure\\Configuration\\Write\\TenantWriteConfiguration.cs": "clone_full",
    "src\\MyNewApp.Application\\Features\\Tenant\\Command\\Create": "clone_full",
    "src\\MyNewApp.Application\\Features\\Tenant\\Command\\Delete": "clone_full",
    "src\\MyNewApp.Application\\Features\\Tenant\\Command\\Update": "clone_full",
    "src\\MyNewApp.Application\\Features\\Tenant\\Query\\Get": "clone_full",
    "src\\MyNewApp.Application\\Features\\Tenant\\Query\\List": "clone_full",
    "src\\MyNewApp.Application\\Features\\Tenant\\Query\\NVList": "clone_full",
    "src\\MyNewApp.Infrastructure\\Services": "skip",
    "src\\MyNewApp.Infrastructure\\Seeds": "skip",
    "src\\MyNewApp.Infrastructure\\Helpers": "skip",
    "src\\MyNewApp.Infrastructure\\Extensions\\ServiceCollectionExtensions.cs": "add_registration",
    "src\\MyNewApp.Infrastructure\\Migrations": "add_migration"
}

destination_folder = r"D:\OfficeProjects\ProjectGenerator\MyNewApp"

entities = [
    {
        "name": "Product",
        "template_path": r"D:\OfficeProjects\ProjectGenerator\NewEntity\Product.cs"
    },
    {
        "name": "Order",
        "template_path": r"D:\OfficeProjects\ProjectGenerator\NewEntity\Order.cs"
    },
    {
        "name": "OrderDetail",
        "template_path": r"D:\OfficeProjects\ProjectGenerator\NewEntity\OrderDetail.cs"
    },
    # Add more entities as needed
]
destination_folder = r"D:\OfficeProjects\ProjectGenerator\MyNewApp"
    # clone entity
for entity in entities:
    print(f"🔁 Processing {entity['name']}")
    clone_entity_from_project_with_decisions(
        destination_folder,
        entity["template_path"],
        "Tenant",  # Template base entity name
        decisions
    )