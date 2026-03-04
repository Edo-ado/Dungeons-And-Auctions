

(function () {
    'use strict';


    function injectStyles() {
        if (document.getElementById('dnd-loader-style')) return;

        const style = document.createElement('style');
        style.id = 'dnd-loader-style';
        style.textContent = `
            /* ── Overlay base ── */
            #dnd-loader {
                position: fixed;
                inset: 0;
                z-index: 99998;
                background: radial-gradient(ellipse at center, #1c0a04 0%, #080302 100%);
                display: flex;
                align-items: center;
                justify-content: center;
                flex-direction: column;
                gap: 36px;
                opacity: 0;
                transition: opacity 0.25s ease;
                pointer-events: all;
            }
            #dnd-loader.visible { opacity: 1; }

            /* ── Wrapper del logo ── */
            .dnd-logo-wrap {
                position: relative;
                display: flex;
                align-items: center;
                justify-content: center;
            }

            /* Logo principal */
            .dnd-logo-img {
                width: 220px;
                height: auto;
                position: relative;
                z-index: 2;
                animation: logo-breathe 2.2s ease-in-out infinite;
            }

            /* Resplandor rojo detrás del logo */
            .dnd-logo-glow {
                position: absolute;
                inset: -30px;
                border-radius: 50%;
                background: radial-gradient(ellipse at center,
                    rgba(192, 57, 43, 0.55) 0%,
                    rgba(139, 26, 26, 0.3)  35%,
                    transparent             70%);
                animation: glow-pulse 2.2s ease-in-out infinite;
                z-index: 1;
            }

            /* Destellos de chispa — partículas flotando */
            .dnd-sparks {
                position: absolute;
                inset: 0;
                z-index: 3;
                pointer-events: none;
            }

            .dnd-spark {
                position: absolute;
                bottom: 20%;
                width: 3px;
                height: 3px;
                border-radius: 50%;
                background: #e8c56a;
                box-shadow: 0 0 4px #c9a84c, 0 0 8px rgba(255,150,50,0.6);
                opacity: 0;
                animation: spark-float var(--dur) ease-in infinite var(--delay);
            }

            /* Llama inferior sutil */
            .dnd-flame-base {
                position: absolute;
                bottom: -10px;
                left: 50%;
                transform: translateX(-50%);
                width: 180px;
                height: 40px;
                background: radial-gradient(ellipse at center bottom,
                    rgba(192, 57, 43, 0.6) 0%,
                    rgba(139, 26, 26, 0.3) 50%,
                    transparent            100%);
                filter: blur(8px);
                animation: flame-flicker 0.4s ease-in-out infinite alternate;
                z-index: 0;
            }

            /* ── Texto ── */
            .dnd-loader-text {
                font-family: 'Cinzel', serif;
                font-size: 11px;
                letter-spacing: 0.3em;
                text-transform: uppercase;
                color: #c9a84c;
                text-shadow:
                    0 0 10px rgba(201,168,76,0.8),
                    0 0 22px rgba(192, 57, 43, 0.5);
                animation: text-ember 2.5s ease-in-out infinite;
            }

            .dnd-dots::after {
                content: '';
                animation: dots-anim 1.5s steps(4, end) infinite;
            }

            /* ── Keyframes ── */
            @keyframes logo-breathe {
                0%, 100% {
                    filter:
                        drop-shadow(0 0 8px  rgba(192, 57, 43, 0.7))
                        drop-shadow(0 0 20px rgba(139, 26, 26, 0.5))
                        drop-shadow(0 0 40px rgba(80,  10, 10, 0.3));
                    transform: scale(1);
                }
                50% {
                    filter:
                        drop-shadow(0 0 18px rgba(220, 80,  30, 0.95))
                        drop-shadow(0 0 40px rgba(192, 57, 43, 0.75))
                        drop-shadow(0 0 70px rgba(139, 26, 26, 0.5))
                        drop-shadow(0 0 5px  rgba(230,180,80, 0.4));
                    transform: scale(1.04);
                }
            }

            @keyframes glow-pulse {
                0%, 100% { opacity: 0.5; transform: scale(0.95); }
                50%       { opacity: 1;   transform: scale(1.08); }
            }

            @keyframes flame-flicker {
                0%   { opacity: 0.6; transform: translateX(-50%) scaleX(0.9); }
                100% { opacity: 1;   transform: translateX(-50%) scaleX(1.1); }
            }

            @keyframes spark-float {
                0%   { opacity: 0;   transform: translateY(0)    translateX(0)    scale(1);   }
                20%  { opacity: 1; }
                80%  { opacity: 0.6; }
                100% { opacity: 0;   transform: translateY(-90px) translateX(var(--dx)) scale(0.3); }
            }

            @keyframes text-ember {
                0%, 100% {
                    opacity: 0.6;
                    text-shadow:
                        0 0 8px  rgba(201,168,76,0.5),
                        0 0 16px rgba(192, 57, 43, 0.3);
                }
                50% {
                    opacity: 1;
                    text-shadow:
                        0 0 14px rgba(201,168,76,0.9),
                        0 0 28px rgba(220, 80, 30, 0.6),
                        0 0 40px rgba(139, 26, 26, 0.4);
                }
            }

            @keyframes dots-anim {
                0%  { content: '';     }
                25% { content: '.';   }
                50% { content: '..';  }
                75% { content: '...'; }
            }
        `;
        document.head.appendChild(style);
    }


    function buildSparks() {
        
        let html = '';
        const positions = [10, 18, 28, 38, 48, 55, 63, 72, 82, 90];
        positions.forEach((left, i) => {
            const dur = (1.2 + Math.random() * 1.4).toFixed(2);
            const delay = (Math.random() * 1.8).toFixed(2);
            const dx = (Math.random() * 40 - 20).toFixed(0);
            html += `<div class="dnd-spark" style="
                left: ${left}%;
                --dur:   ${dur}s;
                --delay: ${delay}s;
                --dx:    ${dx}px;
            "></div>`;
        });
        return html;
    }

    function showLoader() {
        injectStyles();
        if (document.getElementById('dnd-loader')) return;

        const overlay = document.createElement('div');
        overlay.id = 'dnd-loader';
        overlay.innerHTML = `
            <div class="dnd-logo-wrap">
                <div class="dnd-logo-glow"></div>
                <div class="dnd-flame-base"></div>
                <img
                    class="dnd-logo-img"
                    src="/Images/Logo/ShortLogo.png"
                    alt="D&A Logo"
                    draggable="false"
                />
                <div class="dnd-sparks">${buildSparks()}</div>
            </div>
            <div class="dnd-loader-text">
                Viajando al Reino<span class="dnd-dots"></span>
            </div>
        `;

        document.body.appendChild(overlay);
        overlay.getBoundingClientRect();
        overlay.classList.add('visible');
    }

    function hideLoader() {
        const overlay = document.getElementById('dnd-loader');
        if (!overlay) return;
        overlay.classList.remove('visible');
        setTimeout(() => overlay.remove(), 260);
    }


    function navigateTo(url, pushState = true) {
        showLoader();

        fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } })
            .then(res => {
                if (res.redirected) {
                    window.location.href = res.url;
                    return null;
                }
                return res.text();
            })
            .then(html => {
                if (!html) return;

                const parser = new DOMParser();
                const doc = parser.parseFromString(html, 'text/html');

                const newMain = doc.querySelector('main.flex-grow');
                const curMain = document.querySelector('main.flex-grow');

                if (newMain && curMain) {
                    curMain.innerHTML = newMain.innerHTML;
                }

                const newTitle = doc.querySelector('title');
                if (newTitle) document.title = newTitle.textContent;

                if (pushState) history.pushState({}, '', url);

              
                curMain?.querySelectorAll('script').forEach(oldScript => {
                    const s = document.createElement('script');
                    if (oldScript.src) {
                        s.src = oldScript.src;
                    } else {
                        s.textContent = oldScript.textContent;
                    }
                    document.body.appendChild(s);
                    oldScript.remove();
                });

                hideLoader();
                window.scrollTo({ top: 0, behavior: 'smooth' });
            })
            .catch(() => {
                hideLoader();
                window.location.href = url;
            });
    }

    function init() {
        document.addEventListener('click', function (e) {
            const link = e.target.closest('a');

            if (!link) return;
            if (link.target === '_blank') return;
            if (link.hostname !== location.hostname) return;
            if (link.pathname === location.pathname &&
                link.search === location.search) return;
            if (link.getAttribute('href')?.startsWith('#')) return;
            if (link.hasAttribute('data-no-ajax')) return;

            e.preventDefault();
            navigateTo(link.href);
        });

        window.addEventListener('popstate', function () {
            navigateTo(location.href, false);
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

})();