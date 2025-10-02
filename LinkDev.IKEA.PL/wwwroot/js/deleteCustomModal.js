//document.addEventListener("DOMContentLoaded", function () {
//    const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');

//    popoverTriggerList.forEach(triggerEl => {
//        const popover = new bootstrap.Popover(triggerEl, {
//            html: true,
//            content: function () {
//                const id = triggerEl.dataset.deptid;
//                return `
//                    <div class="list-group">
//                        <a href="/Department/Details/${id}"
//                           class="list-group-item list-group-item-action">
//                           <i class="fas fa-info-circle me-2"></i>Details
//                        </a>
//                        <a href="/Department/Edit/${id}"
//                           class="list-group-item list-group-item-action">
//                           <i class="fas fa-edit me-2"></i>Edit
//                        </a>
//                        <a href="#"
//                           id="deleteAction"
//                           data-id="${id}"
//                           class="list-group-item list-group-item-action text-danger">
//                           <i class="fas fa-trash me-2"></i>Delete
//                        </a>
//                    </div>
//                `;
//            }
//        });

//        // hook delete button after popover is shown
//        triggerEl.addEventListener('shown.bs.popover', () => {
//            const deleteBtn = document.querySelector('#deleteAction');
//            if (deleteBtn) {
//                deleteBtn.addEventListener('click', () => {
//                    const deptId = deleteBtn.getAttribute('data-id');
//                    console.log(deptId);
//                    // put deptId into hidden input in modal
//                    document.getElementById('deleteDepartmentId').value = deptId;
//                    document.getElementById('deleteModalBody').textContent =
//                        `Are you sure you want to delete Department #${deptId}?`;

//                    // show the modal
//                    const modal = new bootstrap.Modal(document.getElementById('exampleModal'));
//                    modal.show();
//                });
//            }
//        });
//    });
//});
//document.addEventListener("click", function (e) {
//    const btn = e.target.closest("#deleteModalBtn");
//    if (btn) {
//        const deptId = btn.dataset.deptid;
//        document.getElementById("deleteDeptId").value = deptId;
//        console.log("Selected Department ID:", deptId);
//    }
//});

document.addEventListener("DOMContentLoaded", function () {
    // 1. Initialize all popovers
    const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
    [...popoverTriggerList].map(el => new bootstrap.Popover(el));

    // 2. Handle delete buttons inside popovers
    document.addEventListener("click", function (e) {
        const btn = e.target.closest(".deleteModalBtn");
        if (btn) {
            const deptId = btn.dataset.deptid;
            document.getElementById("deleteDeptId").value = deptId;
            console.log("Selected Department ID:", deptId); // for debugging
        }
    });
});