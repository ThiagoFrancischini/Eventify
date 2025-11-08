window.gerarQRCode = (elementoId, pixString) => {

    const qrElemento = document.getElementById(elementoId);

    if (qrElemento) {
        qrElemento.innerHTML = "";

        new QRCode(qrElemento, {
            text: pixString,
            width: 220,
            height: 220,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRCode.CorrectLevel.H
        });
    }
};