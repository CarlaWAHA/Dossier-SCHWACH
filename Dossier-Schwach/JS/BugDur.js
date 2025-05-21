document.addEventListener("DOMContentLoaded", function () {
    // Texte à afficher lettre par lettre
    const textToDisplay = "Maintenant que vous savez tout .... Vous êtes à moi";
    const animationSpeed = 100; // Temps entre chaque lettre (en millisecondes)
    const delayBeforeAnimation = 2000; // Délai avant de commencer l'animation (en millisecondes)

    function typeText() {
        // Crée un conteneur semi-transparent couvrant toute la page
        const overlay = document.createElement("div");
        overlay.style.position = "fixed";
        overlay.style.top = "0";
        overlay.style.left = "0";
        overlay.style.width = "100%";
        overlay.style.height = "100%";
        overlay.style.backgroundColor = "rgba(0, 0, 0, 0.5)"; // Semi-transparent
        overlay.style.zIndex = "9998"; // Derrière le texte
        document.body.appendChild(overlay);

        // Crée un conteneur pour le texte
        const textContainer = document.createElement("div");
        textContainer.style.position = "fixed";
        textContainer.style.top = "50%";
        textContainer.style.left = "50%";
        textContainer.style.transform = "translate(-50%, -50%)";
        textContainer.style.color = "white";
        textContainer.style.fontSize = "2em";
        textContainer.style.fontFamily = "'Courier New', Courier, monospace";
        textContainer.style.textAlign = "center";
        textContainer.style.backgroundColor = "rgba(0, 0, 0, 0.8)";
        textContainer.style.padding = "20px";
        textContainer.style.borderRadius = "10px";
        textContainer.style.zIndex = "9999"; // Au-dessus de l'overlay
        document.body.appendChild(textContainer);

        let index = 0;

        function displayNextLetter() {
            if (index < textToDisplay.length) {
                textContainer.textContent += textToDisplay[index];
                index++;
                setTimeout(displayNextLetter, animationSpeed);
            } else {
                // Une fois le texte affiché, fermer la page après un délai
                setTimeout(function () {
                    window.close(); // Ferme la page
                }, 3000); // Délai avant de fermer la page (3 secondes)
            }
        }

        displayNextLetter();
    }

    // Lance l'animation après le délai
    setTimeout(typeText, delayBeforeAnimation);
});