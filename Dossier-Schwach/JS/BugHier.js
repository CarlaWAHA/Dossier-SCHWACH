if (window.location.pathname.includes('IS-1989.html')) {
    localStorage.setItem('visitedIS1989', 'true');
}

if (window.location.pathname.includes('Hierarchie.html')) {
    if (localStorage.getItem('visitedIS1989') === 'true') {
        const flashImage = document.createElement('div');
        flashImage.id = 'flash-image';
        flashImage.style.position = 'fixed';
        flashImage.style.top = '0';
        flashImage.style.left = '0';
        flashImage.style.width = '100%';
        flashImage.style.height = '100%';
        flashImage.style.background = 'rgba(255, 255, 255, 0.9)';
        flashImage.style.zIndex = '9999';
        flashImage.style.display = 'flex';
        flashImage.style.justifyContent = 'center';
        flashImage.style.alignItems = 'center';

        const img = document.createElement('img');
        img.src = '../Image/necro.jpg';
        img.alt = 'Flash Image';
        img.style.maxWidth = '90%';
        img.style.maxHeight = '90%';

        flashImage.appendChild(img);
        document.body.appendChild(flashImage);

        setTimeout(() => {
            flashImage.remove();
        }, 10000);
    }
}