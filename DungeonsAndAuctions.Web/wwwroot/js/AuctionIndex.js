document.querySelectorAll('.filter-pill').forEach(pill => {
    pill.addEventListener('click', function () {
        document.querySelectorAll('.filter-pill').forEach(p => p.classList.remove('active'));
        this.classList.add('active');
        const tab = this.dataset.tab;
        document.querySelectorAll('.tab-panel').forEach(panel => {
            panel.classList.toggle('hidden', panel.id !== `tab-${tab}`);
        });
    });
});