$(document).ready(function () {
    $('#subastas-toggle').on('click', function (e) {
        e.stopPropagation();
        $('#subastas-dropdown').slideToggle(200);
        $('#subastas-arrow').toggleClass('rotate-180');
    });
    $(document).on('click', function () {
        $('#subastas-dropdown').slideUp(200);
        $('#subastas-arrow').removeClass('rotate-180');
    });
    $('#subastas-dropdown').on('click', function (e) {
        e.stopPropagation();
    });
});


document.querySelectorAll('a, button').forEach(el => {
    el.addEventListener('mouseenter', () => {
        const sound = document.getElementById('hover-sound');
        sound.currentTime = 0;
        sound.volume = 0.3;
        sound.play().catch(() => { });
    });
});