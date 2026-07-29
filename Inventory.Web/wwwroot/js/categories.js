document.querySelectorAll(".btn-view")
    .forEach(button => {

        button.addEventListener("click", async () => {

            const id = button.dataset.id;

            const response =
                await fetch(`api/Category/${id}`);

            const category =
                await response.json();

            document.getElementById("detailId").innerText =
                category.id;

            document.getElementById("detailName").innerText =
                category.name;

            document.getElementById("detailDescription").innerText =
                category.description;

            document.getElementById("detailStatus").innerText =
                category.active ? "Active" : "Inactive";

            new bootstrap.Modal(
                document.getElementById("detailModal"))
                .show();

        });

    });