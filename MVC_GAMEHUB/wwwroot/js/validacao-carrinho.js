document.querySelector('form').addEventListener('submit', function (e) {
    document.querySelectorAll('.erro-js').forEach(function (el) { el.remove(); });

    const selecionado = document.querySelector('input[name="pagamento"]:checked');

    if (!selecionado) {
        e.preventDefault();
        const container = document.querySelector('input[name="pagamento"]').parentNode.parentNode;
        const erro = document.createElement('div');
        erro.className = 'erro-js';
        erro.style.cssText = 'color:#ff6b6b; font-size:11px; margin-top:8px;';
        erro.textContent = 'Selecione uma forma de pagamento.';
        container.appendChild(erro);
    }
});