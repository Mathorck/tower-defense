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
        
        if ($files.Count -gt 0) {
            # Ajouter une barre de séparation au début de chaque groupe
            Add-Content -Path $output_file -Value "`n---------------------------------`n"
            
            # Ajouter la déclaration du tableau pour le monstre et l'action
            Add-Content -Path $output_file -Value "Texture2D[] monstre$i$action = new Texture2D[]`n{"
        }
        
        foreach ($file in $files) {
            # Ajouter chaque chemin sous forme de tableau
            $output = "`tRaylib.LoadTexture('$($file.FullName)'),"
            Add-Content -Path $output_file -Value $output
        }

        if ($files.Count -gt 0) {
            # Fermer le tableau avec une accolade
            Add-Content -Path $output_file -Value "};`n"
        }
    }
}

# Afficher un message à la fin
Write-Host "Chemins générés dans le fichier $output_file"
