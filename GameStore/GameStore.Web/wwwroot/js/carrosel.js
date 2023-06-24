window.addEventListener('load', function () {
    var carrossel = document.getElementById('carrossel');
    var imagens = carrossel.getElementsByTagName('img');
    var indiceAtual = 0;

    // Exibe a primeira imagem
    imagens[indiceAtual].style.display = 'block';

    // Alterna para a próxima imagem a cada 3 segundos
    setInterval(function () {
        imagens[indiceAtual].style.display = 'none';
        indiceAtual = (indiceAtual + 1) % imagens.length;
        imagens[indiceAtual].style.display = 'block';
    }, 3000);
});