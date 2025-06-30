import os
import re
import shutil
import subprocess  # Make sure it's at the top

def copy_project_selectively(src_folder, dest_folder, include_exts=None, exclude_exts=None, exclude_dirs=None):
    for root, dirs, files in os.walk(src_folder):
        # Get the relative folder path from source
        relative_path = os.path.relpath(root, src_folder)

        # Check if this folder or its ancestors are in the exclude list
        if exclude_dirs:
            # Normalize paths for comparison
            normalized_parts = set(os.path.normpath(root).split(os.sep))
            if any(ex.lower() in normalized_parts for ex in exclude_dirs):
                print(f"Skipped folder: {root}")
                continue

        # Create corresponding folder in destination
        dest_path = os.path.join(dest_folder, relative_path)
        os.makedirs(dest_path, exist_ok=True)

        for file in files:
            file_ext = os.path.splitext(file)[1].lower()
            should_copy = True

            # Skip excluded file types
            if exclude_exts and file_ext in exclude_exts:
                should_copy = False

            # Skip if not in included types
            if include_exts and file_ext not in include_exts:
                should_copy = False

            if should_copy:
                src_file = os.path.join(root, file)
                dest_file = os.path.join(dest_path, file)
                shutil.copy2(src_file, dest_file)
                print(f"Copied: {src_file} → {dest_file}")
            else:
                print(f"Skipped file: {file}")



def detect_namespace(project_folder):
    pattern = re.compile(r'^\s*namespace\s+([a-zA-Z0-9_.]+)', re.MULTILINE)

    for root, _, files in os.walk(project_folder):
        for file in files:
            if file.lower().endswith('.cs'):
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, 'r', encoding='utf-8') as f:
                        content = f.read()
                        match = pattern.search(content)
                        if match:
                            return match.group(1)
                except:
                    continue
    return None

def replace_namespace_in_files(target_folder, old_namespace, new_namespace, file_extensions):
    for root, _, files in os.walk(target_folder):
        for file in files:
            if any(file.lower().endswith(ext) for ext in file_extensions):
                file_path = os.path.join(root, file)
                try:
                    with open(file_path, 'r', encoding='utf-8') as f:
                        content = f.read()

                    updated_content = content.replace(old_namespace, new_namespace)

                    if updated_content != content:
                        with open(file_path, 'w', encoding='utf-8') as f:
                            f.write(updated_content)
                        print(f"✅ Updated file: {file_path}")
                except Exception as e:
                    print(f"❌ Failed to update {file_path}: {e}")

def rename_namespace_folder_structure(base_path, old_namespace, new_namespace):
    renamed = False
    for root, dirs, _ in os.walk(base_path):
        for dir_name in dirs:
            if old_namespace in dir_name:
                old_path = os.path.join(root, dir_name)
                new_dir_name = dir_name.replace(old_namespace, new_namespace)
                new_path = os.path.join(root, new_dir_name)

                try:
                    os.rename(old_path, new_path)
                    print(f"📁 Renamed:\n{old_path}\n→\n{new_path}")
                    renamed = True
                except Exception as e:
                    print(f"❌ Rename failed: {old_path} → {e}")
    if not renamed:
        print(f"⚠️ No folder matched namespace part: {old_namespace}")

def rename_files_with_namespace(target_folder, old_namespace, new_namespace):
    for root, _, files in os.walk(target_folder):
        for file_name in files:
            if old_namespace in file_name:
                old_file_path = os.path.join(root, file_name)
                new_file_name = file_name.replace(old_namespace, new_namespace)
                new_file_path = os.path.join(root, new_file_name)
                try:
                    os.rename(old_file_path, new_file_path)
                    print(f"📝 Renamed file: {old_file_path} → {new_file_path}")
                except Exception as e:
                    print(f"❌ Failed to rename file: {old_file_path} → {e}")



def rename_namespace_flat_folders(base_path, old_namespace, new_namespace):
    for root, dirs, _ in os.walk(base_path, topdown=False):  # bottom-up
        for dir_name in dirs:
            if old_namespace in dir_name:
                old_dir_path = os.path.join(root, dir_name)
                new_dir_name = dir_name.replace(old_namespace, new_namespace)
                new_dir_path = os.path.join(root, new_dir_name)

                if old_dir_path != new_dir_path and not os.path.exists(new_dir_path):
                    try:
                        os.rename(old_dir_path, new_dir_path)
                        print(f"📁 Renamed folder:\n  From: {old_dir_path}\n  To:   {new_dir_path}")
                    except Exception as e:
                        print(f"❌ Failed to rename folder: {old_dir_path} → {e}")





# === USAGE ===
source_folder = r"D:\OfficeProjects\ProjectGenerator\TemplateProject\ProjectName"
destination_folder = r"D:\OfficeProjects\ProjectGenerator\MyNewApp"

# File extensions you want to include (use None to include all)
include_extensions = {'.props','.csproj.user','.cs', '.json', '.csproj', '.config','.sln'}

# File extensions you want to skip
exclude_extensions = {'.pdb','.gitattributes','.md','.yml'}

# Folder names (not full paths) you want to exclude (case-insensitive)
exclude_directories = {'bin', 'obj', '.vs', 'node_modules','.github','translations','Logs','.git'}

# Run the function
copy_project_selectively(
    source_folder,
    destination_folder,
    include_exts=include_extensions,
    exclude_exts=exclude_extensions,
    exclude_dirs=exclude_directories
)

# now change name space# === After copying the project ===

file_types_to_edit = ['.cs', '.csproj', '.cshtml', '.json', '.config','.sln']

# Auto-detect old namespace
old_ns ='ProjectName'# detect_namespace(destination_folder)
if old_ns:
    print(f"📌 Detected old namespace: {old_ns}")
    new_ns = "MyNewApp"

    # Replace in files
    replace_namespace_in_files(destination_folder, old_ns, new_ns, file_types_to_edit)

    # Rename folders
    rename_namespace_flat_folders(destination_folder, old_ns, new_ns)
    rename_files_with_namespace(destination_folder, old_ns, new_ns)
    # clone entity

    print("✅ Namespace replacement complete.")
else:
    print("❌ Could not detect any namespace.")


