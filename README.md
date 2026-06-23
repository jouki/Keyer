# Keyer

A tiny Windows utility that **automatically formats a list of codes from your clipboard** and assigns each code to a person, then puts the formatted result straight back on your clipboard — ready to paste.

It reads codes either from **plain text** already on the clipboard, or from a **`.csv` / `.xlsx` file** whose path is on the clipboard (e.g. when you copy a file in Explorer).

## How it works

1. **Put your codes on the clipboard** — in one of these ways:
   - **Plain text** — codes copied as text, one per line.
   - **A file** — copy a `.csv` or `.xlsx` file in Explorer (Ctrl+C). Keyer reads the first column / first worksheet.
2. **Run `Keyer.exe`.**
3. **Pick the starting admin** — type the number of the admin who should receive the *second* code, then press Enter.
4. **Paste the result.** Keyer builds a formatted list and copies it back to the clipboard, wrapped in a code block:

   ```
   CODE-A - Doc
   CODE-B - Jouki
   CODE-C - ILeonx
   ```

The first codes are handed out to the fixed list of admins in order; any remaining ("extra") codes are distributed cyclically among the admins, starting from the admin you selected.

> **Note:** the list of names and the number of extra codes are currently hard-coded in [`Keyer/Program.cs`](Keyer/Program.cs). Edit them there and rebuild to customise.

## Download

Grab the latest **`Keyer.exe`** from the [Releases page](https://github.com/jouki/Keyer/releases/latest).

It's a single self-contained executable — no installation, the required libraries are bundled in.

### Requirements

- Windows with **.NET Framework 4.7.2** (pre-installed on current Windows versions).

### ⚠️ Antivirus false positive

Because `Keyer.exe` is **unsigned** and bundles its dependencies into a **single self-contained executable** (via Costura.Fody), some antivirus engines (e.g. ESET, Windows Defender) may flag it with a generic heuristic detection. **This is a false positive** — the full source is in this repository and the executable is built automatically by the [GitHub Actions workflow](.github/workflows/release.yml), so you can verify exactly what goes into it.

If your antivirus blocks it, you can:

- **Allow / restore** the file in your antivirus (e.g. in ESET: *Tools → Log files → Detected threats*, then add a detection exclusion), or
- **Build it yourself** from source (see below) and run your own copy.

## Building from source

Requires Visual Studio 2022 (or MSBuild) with .NET Framework 4.7.2 targeting pack.

```powershell
nuget restore Keyer.sln
msbuild Keyer.sln /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild
```

The dependencies are embedded into a single self-contained `Keyer.exe` automatically during the build by [Costura.Fody](https://github.com/Fody/Costura), so the build output `Keyer\bin\Release\Keyer.exe` is already a standalone file.

The [`.github/workflows/release.yml`](.github/workflows/release.yml) workflow builds and publishes a release automatically on every `v*` tag.

## License

[MIT](LICENSE)
