document.addEventListener("DOMContentLoaded", function () {
    // Generar un puntaje crediticio aleatorio entre 300 y 850
    const creditScore = Math.floor(Math.random() * (850 - 300 + 1)) + 300;
    const creditScoreElement = document.getElementById("credit-score");

    // Determinar la categoría del puntaje
    let category = "";
    if (creditScore <= 550) {
        category = "Bajo";
        creditScoreElement.classList.add("low-score");
    } else if (creditScore <= 650) {
        category = "Regular";
        creditScoreElement.classList.add("regular-score");
    } else if (creditScore <= 700) {
        category = "Buena";
        creditScoreElement.classList.add("good-score");
    } else {
        category = "Excelente";
        creditScoreElement.classList.add("excellent-score");
    }

    // Mostrar el puntaje y la categoría
    creditScoreElement.innerHTML = `Puntaje: ${creditScore} - ${category}`;
});
