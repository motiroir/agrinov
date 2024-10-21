// Parallax
let bg = document.getElementById('bg')
let montagnes = document.getElementById('montagnes')
let arbres3 = document.getElementById('arbres3')
let arbres2 = document.getElementById('arbres2')
let arbres1 = document.getElementById('arbres1')
let front2 = document.getElementById('front2')
let front1 = document.getElementById('front1')
let text = document.getElementById('text')

window.addEventListener('scroll', () => {
    let value = window.scrollY;

    if (value <= 300) { 
        bg.style.marginTop = value * 0.9  + 'px';  
        montagnes.style.marginTop = value * 0.8 + 'px';  
        arbres3.style.marginTop = value * 0.6 + 'px';   
        arbres2.style.marginTop = value * 0.5 + 'px';   
        arbres1.style.marginTop = value * 0.4 + 'px';   
        front2.style.marginTop = value * 0.3 + 'px';    
        front1.style.marginTop = value * 0.2 + 'px';     
        text.style.marginTop = value * 0 + 'px';
    } else {
        bg.style.marginTop = 300 * 0.9 + 'px';
        montagnes.style.marginTop = 300 * 0.8 + 'px';  
        arbres3.style.marginTop = 300 * 0.6 + 'px';   
        arbres2.style.marginTop = 300 * 0.5 + 'px';   
        arbres1.style.marginTop = 300 * 0.4 + 'px';   
        front2.style.marginTop = 300 * 0.3 + 'px';    
        front1.style.marginTop = 300 * 0.2 + 'px';     
        text.style.marginTop = 300 * 0 + 'px';
    }
});


// Carousel
document.addEventListener('DOMContentLoaded', () => {
    const radios = document.querySelectorAll('input[name="position"]');
    const carousel = document.getElementById('carousel');
    const items = carousel.querySelectorAll('.item');
    const photos = carousel.querySelectorAll('.photo');
    const cards = carousel.querySelectorAll('.card-subcontainer');
    const contents = carousel.querySelectorAll('.card-content');
    const prevButton = document.getElementById('prev');
    const nextButton = document.getElementById('next');

    let currentPosition = 5;
    
    const updateCarousel = (position) => {
        items.forEach((item) => {
            item.style.filter = 'blur(5px)'; 
            item.style.height = '400px';
            item.style.width = '700px'; 
        });
        photos.forEach((photo) => {
            photo.style.height = '160px'; 
            photo.style.width = '160px';
        });
        cards.forEach((card) => { 
            card.style.height = '170px';
            card.style.width = '600px';
        });
        contents.forEach((content) => {
            content.style.height = '170px';
        });

        const selectedItem = items[position - 1];
        const selectedPhoto = photos[position - 1];
        const selectedCard = cards[position - 1];
        const selectedContent = contents[position - 1];

        selectedItem.style.filter = 'blur(0px)'; 
        selectedItem.style.height = '450px'; 
        selectedItem.style.width = '800px';

        selectedPhoto.style.height= '260px'; 
        selectedPhoto.style.width = '260px';

        selectedCard.style.height = '272px';
        selectedCard.style.width= '743px';        

        selectedContent.style.height = '270px';

        carousel.style.setProperty('--position', position);
        currentPosition = position;
    };

    updateCarousel(currentPosition);

    radios.forEach((radio) => {
        radio.addEventListener('change', () => {
            const position = parseInt(radio.id.replace('r', ''));
            updateCarousel(position);
        });
    });

    prevButton.addEventListener('click', () => {
        if (currentPosition > 1) {
            currentPosition--;
        } else {
            currentPosition = radios.length;
        }
        updateCarousel(currentPosition);
        document.getElementById(`r${currentPosition}`).checked = true;
    });

    nextButton.addEventListener('click', () => {
        if (currentPosition < radios.length) {
            currentPosition++;
        } else {
            currentPosition = 1;
        }
        updateCarousel(currentPosition);
        document.getElementById(`r${currentPosition}`).checked = true;
    });

    
});

