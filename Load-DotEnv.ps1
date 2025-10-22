# Load-DotEnv.ps1
# Reads a .env file and loads the variables into a hashtable

param(
    [string]$FilePath = ".env"
)

# Initialize the hashtable
$dotenv = @{}

# Check if file exists
if (-not (Test-Path $FilePath)) {
    Write-Warning "File not found: $FilePath"
    return $dotenv
}

# Read the .env file line by line
Get-Content $FilePath | ForEach-Object {
    # Match lines in KEY=VALUE format, ignoring comments and empty lines
    if ($_ -match '^(?!#)(?<key>[^=]+)=(?<value>.+)$') {
        $key = $matches['key'].Trim()
        $value = $matches['value'].Trim()
        $dotenv[$key] = $value
    }
}

# Return the hashtable
return $dotenv