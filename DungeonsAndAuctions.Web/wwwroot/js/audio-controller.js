
(function () {
    'use strict';

   
    const audio = document.getElementById('bg-audio');
    const panel = document.getElementById('audio-panel');
    const toggleBtn = document.getElementById('audio-toggle-btn');
    const playPauseBtn = document.getElementById('play-pause-btn');
    const volumeSlider = document.getElementById('volume-slider');
    const volumeDisplay = document.getElementById('volume-display');
    const visualizer = document.getElementById('audio-visualizer');
    const iconSound = document.getElementById('audio-icon-sound');
    const iconMuted = document.getElementById('audio-icon-muted');

    if (!audio || !toggleBtn) return;


    const STATE_KEY = 'audioCtrl_v1';

    const state = loadState() || {
        volume: 0.6,
        playing: true,
        panelOpen: true,
    };


    function init() {
        audio.volume = state.volume;
        volumeSlider.value = Math.round(state.volume * 100);
        volumeDisplay.textContent = Math.round(state.volume * 100) + '%';

        if (state.panelOpen) openPanel();

        updatePlayButton();
        updateToggleIcon();

        if (state.playing) attemptPlay();
    }

    function attemptPlay() {
        const promise = audio.play();
        if (promise !== undefined) {
            promise
                .then(() => {
                    state.playing = true;
                    updatePlayButton();
                    updateToggleIcon();
                    saveState();
                })
                .catch(() => {
                    state.playing = false;
                    updatePlayButton();
                });
        }
    }

    function togglePlayPause() {
        if (audio.paused) {
            attemptPlay();
        } else {
            audio.pause();
            state.playing = false;
            updatePlayButton();
            updateToggleIcon();
            saveState();
        }
    }

    function updatePlayButton() {
        if (!audio.paused) {
            playPauseBtn.textContent = '⏸ Pausar';
            visualizer.classList.add('audio-playing');
            toggleBtn.classList.add('playing');
        } else {
            playPauseBtn.textContent = '▶ Reproducir';
            visualizer.classList.remove('audio-playing');
            toggleBtn.classList.remove('playing');
        }
    }

   
    function setVolume(value) {
        const vol = parseInt(value, 10) / 100;
        audio.volume = vol;
        audio.muted = vol === 0;
        state.volume = vol;
        volumeDisplay.textContent = value + '%';
        updateToggleIcon();
        saveState();
    }

    function updateToggleIcon() {
        const isMuted = audio.muted || audio.volume === 0 || audio.paused;
        if (isMuted) {
            iconSound.style.display = 'none';
            iconMuted.style.display = 'block';
            toggleBtn.classList.add('muted');
        } else {
            iconSound.style.display = 'block';
            iconMuted.style.display = 'none';
            toggleBtn.classList.remove('muted');
        }
    }

 
    function openPanel() { panel.classList.add('open'); state.panelOpen = true; saveState(); }
    function closePanel() { panel.classList.remove('open'); state.panelOpen = false; saveState(); }
    function togglePanel() { panel.classList.contains('open') ? closePanel() : openPanel(); }

    document.addEventListener('click', function (e) {
        const controller = document.getElementById('audio-controller');
        if (controller && !controller.contains(e.target)) closePanel();
    });

   
    function saveState() {
        try { localStorage.setItem(STATE_KEY, JSON.stringify(state)); } catch (_) { }
    }

    function loadState() {
        try {
            const raw = localStorage.getItem(STATE_KEY);
            return raw ? JSON.parse(raw) : null;
        } catch (_) { return null; }
    }

 
    toggleBtn.addEventListener('click', (e) => { e.stopPropagation(); togglePanel(); });
    playPauseBtn.addEventListener('click', togglePlayPause);
    volumeSlider.addEventListener('input', function () { setVolume(this.value); });

    audio.addEventListener('play', updatePlayButton);
    audio.addEventListener('pause', updatePlayButton);
    audio.addEventListener('ended', updatePlayButton);

  
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

})();