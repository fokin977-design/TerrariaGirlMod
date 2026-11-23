# Decodes b64/*.b64 files back into real PNG/GIF binaries.
# Usage: powershell -File decode.ps1
New-Item -ItemType Directory -Force -Path . | Out-Null
Get-ChildItem "b64\*.b64" | ForEach-Object {
    $target = $_.Name -replace '\.b64$',''
    [IO.File]::WriteAllBytes($target, [Convert]::FromBase64String((Get-Content $_.FullName -Raw)))
    Write-Host "decoded -> $target"
}
