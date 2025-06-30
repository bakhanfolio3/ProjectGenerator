import subprocess
import os

def build_solution(sln_path):
    if not os.path.exists(sln_path):
        print(f"❌ Solution file not found: {sln_path}")
        return False

    print(f"🔧 Building solution: {sln_path}")
    command = ["dotnet", "build", sln_path, "--configuration", "Debug"]

    result = subprocess.run(command, capture_output=True, text=True)
    print("🔍 Build Output:\n", result.stdout)

    if result.returncode == 0:
        print("✅ Solution build succeeded.")
        return True
    else:
        print("❌ Solution build failed:\n", result.stderr)
        return False


def run_startup_project(startup_project_path):
    if not os.path.exists(startup_project_path):
        print(f"❌ Startup project not found: {startup_project_path}")
        return

    print(f"🚀 Running startup project: {startup_project_path}")
    command = ["dotnet", "run", "--project", startup_project_path]

    process = subprocess.Popen(command, stdout=subprocess.PIPE, stderr=subprocess.STDOUT, text=True)

    try:
        for line in process.stdout:
            print(line.strip())
    except Exception as e:
        print(f"❌ Error while running: {e}")

    process.wait()
    if process.returncode == 0:
        print("✅ Project run completed.")
    else:
        print("❌ Project run failed.")


if __name__ == "__main__":
    sln_path = r"D:\OfficeProjects\ProjectGenerator\MyNewApp\MyNewApp.sln"
    startup_project = r"D:\OfficeProjects\ProjectGenerator\MyNewApp\src\MyNewApp.Api\MyNewApp.Api.csproj"  # 👈 Update if needed

    if build_solution(sln_path):
        run_startup_project(startup_project)
