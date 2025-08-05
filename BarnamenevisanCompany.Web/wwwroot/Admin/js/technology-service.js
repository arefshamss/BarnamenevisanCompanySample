class Technology {
    static TechnologyIds = [];
    constructor(id) {
        this.id = id;
    }

    // unused
    static GetById(id) {
        return Technology.TechnologyIds.find(s => s.id === id);
    }

    static AddToList(id) {
        Technology.TechnologyIds.push(new Technology(id));
    }

    static GetAllIdsSplitByComma() {
        return Technology.TechnologyIds.map(s => s.id).join(",");
    }

    static SaveChanges() {
        $("#TechnologyIds").val(this.GetAllIdsSplitByComma());
    }

    static SaveChangesAndHideModal() {
        this.SaveChanges();
        this.UpdateInputCount();
        $("#modal-center-lg").modal("hide");
    }

    static FillFromModel() {
        try {
            this.TechnologyIds = [];
            const TechnologyIdsSeperatedByComma = $("#TechnologyIds").val();
            const TechnologyIds = TechnologyIdsSeperatedByComma.trim().split(",").map(TechnologyIds => Number(TechnologyIds));
            for (let id of TechnologyIds) {
                if (id > 0) {
                    Technology.AddToList(id);
                }
            }
        } catch (ex) {
            console.error(ex, "failed to add role id")
        }
    }

    static Add(event, id) {
        this.AddToList(id);
        let targetElement = $(event.target).is("a") ? $(event.target) : $(event.target.parentNode);

        targetElement
            .removeClass("text-success")
            .addClass("text-danger")
            .attr("onclick", `Technology.Remove(event , ${id})`)
            .html('<iconify-icon icon="hugeicons:minus-sign-square" width="24" height="24"></iconify-icon>');
    }

    static Remove(event, id) {
        const index = Technology.TechnologyIds.findIndex(s => s.id === id);
        if (index !== -1) {
            Technology.TechnologyIds.splice(index, 1);
            let targetElement = $(event.target).is("a") ? $(event.target) : $(event.target.parentNode);

            targetElement
                .removeClass("text-danger")
                .addClass("text-success")
                .attr("onclick", `Technology.Add(event , ${id})`)
                .html('<iconify-icon icon="hugeicons:plus-sign-square" width="24" height="24"></iconify-icon>');
        } else {
            console.error("err: index out of range ... ");
        }
    }

    static UpdateTable() {
        $("#technology-body tr td").each(function () {
            for (let technology of Technology.TechnologyIds) {
                if ($(this).attr("id") === `technology-${technology.id}`) {
                    $(this).find(".add-btn")
                        .removeClass("text-success")
                        .addClass("text-danger")
                        .attr("onclick", `Technology.Remove(event , ${technology.id})`)
                        .html('<iconify-icon icon="hugeicons:minus-sign-square" width="24" height="24"></iconify-icon>');
                }
            }
        });
    }
    static UpdateInputCount() {
        let count = Technology.TechnologyIds.length;
        $("#technology-count-input").val(count > 0 ? `${count} تکنولوژی انتخاب شده` : "");
        formatFloatingLabel()
    }

}


$(() => {
    Technology.FillFromModel();
    Technology.UpdateInputCount();
})