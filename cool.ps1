# Définir le chemin de base
$base_path = "D:\tower-defense\images\Monstres"
$actions = @("run", "die")
$output_file = "D:\tower-defense\chemins.txt"

# Supprimer le fichier de sortie s'il existe déjà
if (Test-Path $output_file) {
    Remove-Item $output_file
}

# Boucle pour les monstres (de 1 à 10)
for ($i = 1; $i -le 10; $i++) {
    # Boucle pour les actions (run et die)
    foreach ($action in $actions) {
        # Récupérer les fichiers .png dans le dossier correspondant
        $files = Get-ChildItem -Path "$base_path\$i\$action\" -Filter "*.png" -Recurse
        foreach ($file in $files) {
            # Ajouter le chemin dans le fichier de sortie
            $output = "Raylib.LoadTexture('$($file.FullName)')"
            Add-Content -Path $output_file -Value $output
        }
    }
}

# Afficher un message à la fin
Write-Host "Chemins générés dans le fichier $output_file"
