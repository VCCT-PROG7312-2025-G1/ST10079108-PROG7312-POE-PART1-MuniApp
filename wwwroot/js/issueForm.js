document.addEventListener("DOMContentLoaded", function () {
    const iform = document.getElementById("issueForm");

    if (iform) {
        iform.addEventListener("submit", function (event) {
            event.preventDefault();
            Swal.fire({
                title: "Submitted!", text: "Your issue has been reported successfully.",icon: "success",confirmButtonText: "OK"
            }).then(() => {
                iform.submit();
            });
        });
    }
});