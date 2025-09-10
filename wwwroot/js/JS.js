
function myFunction() {
    var x = document.getElementById("myHeaderSection");
    if (x.className === "headerSection") {
        x.className += " responsive";
    } else {
        x.className = "headerSection";
    }
}


