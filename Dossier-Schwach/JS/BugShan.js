document.addEventListener("DOMContentLoaded", function () {
    const shandrahaElement = document.getElementById("shandraha");

    // Fonction pour lire un cookie
    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
        return null;
    }

    // Fonction pour définir un cookie
    function setCookie(name, value, days) {
        const date = new Date();
        date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
        document.cookie = `${name}=${value}; expires=${date.toUTCString()}; path=/`;
    }

    // Vérifie si c'est la première visite
    const hasVisited = getCookie("visitedShandraha");

    if (!hasVisited) {
        // Si c'est la première visite, déclenche un "bug" après un délai
        setTimeout(function () {
            console.error("Erreur sur la page Shandraha!");
            window.location.href = "../../Cobaye/Note/Dur_shan.html"; // Redirige vers la page de bug
        }, 5000); // Temps avant le "bug" (5 secondes)

        // Marque la page comme visitée
        setCookie("visitedShandraha", "true", 7); // Cookie valable 7 jours
    }

    // Ajoute un événement pour ramener l'utilisateur sur Shandraha après un clic
    if (shandrahaElement) {
        shandrahaElement.addEventListener("click", function () {
            console.log("Retour sur la page Shandraha !");
            setTimeout(function () {
                window.location.href = "Shandraha.html"; // Retourne sur Shandraha.html
            }, 3000); // Temps avant de revenir (3 secondes)
        });
    }
});