document.querySelector('form').addEventListener('submit', function (e) {
    let valido = true;

    document.querySelectorAll('.erro-js').forEach(function (el) { el.remove(); });

    const email = document.querySelector('input[name="Email"]');
    const senha = document.querySelector('input[name="Senha"]');

    if (email.value.trim() === '') {
        mostrarErro(email, 'O email é obrigatório.');
        valido = false;
    } else if (email.value.indexOf('@') < 1 || email.value.lastIndexOf('.') < email.value.indexOf('@')) {
        mostrarErro(email, 'Digite um email válido.');
        valido = false;
    }

    if (senha.value.trim() === '') {
        mostrarErro(senha, 'A senha é obrigatória.');
        valido = false;
    } else if (senha.value.length < 6) {
        mostrarErro(senha, 'A senha deve ter pelo menos 6 caracteres.');
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