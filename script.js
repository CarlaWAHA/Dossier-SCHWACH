document.addEventListener('DOMContentLoaded', function() {
    const stars = document.querySelectorAll('.star');
    const ratingValue = document.getElementById('rating-value');
    let currentRating = 0;

    stars.forEach(star => {
        star.addEventListener('click', function() {
            currentRating = this.getAttribute('data-value');
            updateRating(currentRating);
        });

        star.addEventListener('mouseover', function() {
            updateStars(this.getAttribute('data-value'));
        });

        star.addEventListener('mouseout', function() {
            updateStars(currentRating);
        });
    });

    function updateRating(rating) {
        ratingValue.textContent = `Note : ${rating} étoile(s)`;
    }

    function updateStars(rating) {
        stars.forEach(star => {
            if (star.getAttribute('data-value') <= rating) {
                star.classList.add('active');
            } else {
                star.classList.remove('active');
            }
        });
    }
});

document.addEventListener1('DOMContentLoaded', function() {
    const stars = document.querySelectorAll('.star');
    const ratingValue = document.getElementById('rating-value');
    const commentInput = document.getElementById('comment');
    const submitButton = document.getElementById('submit-btn');
    const commentsList = document.getElementById('comments-list');
    let currentRating = 0;

    // Gestion des étoiles
    stars.forEach(star => {
        star.addEventListener('click', function() {
            currentRating = this.getAttribute('data-value');
            updateStars(currentRating);
            ratingValue.textContent = `Note : ${currentRating} étoile(s)`;
        });

        star.addEventListener('mouseover', function() {
            updateStars(this.getAttribute('data-value'));
        });

        star.addEventListener('mouseout', function() {
            updateStars(currentRating);
        });
    });

    // Soumission du commentaire
    submitButton.addEventListener('click', function() {
        const commentText = commentInput.value.trim();
        if (commentText !== "" && currentRating > 0) {
            addComment(commentText, currentRating);
            commentInput.value = "";
            currentRating = 0;
            updateStars(0);
            ratingValue.textContent = "Note : 0 étoile(s)";
        } else {
            alert("Veuillez sélectionner une note et écrire un commentaire !");
        }
    });

    function updateStars(rating) {
        stars.forEach(star => {
            if (star.getAttribute('data-value') <= rating) {
                star.classList.add('active');
            } else {
                star.classList.remove('active');
            }
        });
    }

    function addComment(comment, rating) {
        const listItem = document.createElement('li');
        listItem.innerHTML = `<strong>Note : ${rating} étoile(s)</strong><p>${comment}</p>`;
        commentsList.appendChild(listItem);
    }
});