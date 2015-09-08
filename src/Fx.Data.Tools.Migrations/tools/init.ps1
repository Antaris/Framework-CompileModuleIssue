param($installPath, $toolsPath, $package, $project)

if (Get-Module | ? Name -EQ FxMigrations) {
	Remove-Module FxMigrations
}

Import-Module (Join-Path $toolsPath FxMigrations.psd1) -DisableNameChecking