document.querySelector('form').addEventListener('submit', function (e) {
    let valido = true;

    document.querySelectorAll('.erro-js').forEach(function (el) { el.remove(); });

    const nome = document.querySelector('input[name="Nome"]');
    const categoria = document.querySelector('input[name="Categoria"]');
    const preco = document.querySelector('input[name="Preco"]');
    const imagemUrl = document.querySelector('input[name="ImagemUrl"]');

    if (nome.value.trim() === '') {
        mostrarErro(nome, 'O nome é obrigatório.');
        valido = false;
    } else if (nome.value.trim().length < 2) {
        mostrarErro(nome, 'O nome deve ter pelo menos 2 caracteres.');
        valido = false;
    }

    if (categoria.value.trim() === '') {
        mostrarErro(categoria, 'A categoria é obrigatória.');
        valido = false;
    }

    if (preco.value.trim() === '') {
        mostrarErro(preco, 'O preço é obrigatório.');
        valido = false;
    } else if (isNaN(preco.value.replace(',', '.'))) {
        mostrarErro(preco, 'Digite um preço válido.');
        valido = false;
    } else if (parseFloat(preco.value.replace(',', '.')) <= 0) {
        mostrarErro(preco, 'O preço deve ser maior que zero.');
        valido = false;
    }

    if (imagemUrl.value.trim() !== '' && imagemUrl.value.indexOf('http') !== 0) {
        mostrarErro(imagemUrl, 'A URL deve começar com http:// ou https://');
        valido = false;
    }

    if (!valido) { e.preventDefault(); }
});

function mostrarErro(campo, mensagem) {
    campo.style.borderColor = '#ff6b6b';
    const erro = document.createElement('div');
    erro.className = 'erro-js';
    erro.style.cssText = 'color:#ff6b6b; font-size:11px; margin-top:4px;';
    erro.textContent = mensagem;
    campo.parentNode.appendChild(erro);
    campo.addEventListener('input', function () {
        campo.style.borderColor = '#4a2d7a';
        erro.remove();
    }, { once: true });
}