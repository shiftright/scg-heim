
var DEBUG = true;

function initNavigators() {
    $(".step-exterior").click(function (event) {
        SR.Heim.Designer.sendMessage("LoadLevel", "fullhouse");
        SetAppStep("exterior");
        return false;
    });

    $(".step-furniture").click(function (event) {
        SR.Heim.Designer.sendMessage("LoadLevel", "interior,top");
        SetAppStep("furniture");
        return false;
    });

    $(".step-wallpaper").click(function (event) {
        SR.Heim.Designer.sendMessage("LoadLevel", "interior,iso");
        SetAppStep("wallpaper");
        return false;
    });

    $(".toggle-maximize").click(function () {
        $("body").toggleClass("view-maximize");
        $("body").removeClass("current-walkthough");
        return false;
    });

    $(".add-window").click(function () {
        SR.Heim.Designer.sendMessage("StartAddingWindow");
        return false;
    });

    $(".walkthrough").click(function () {
        SR.Heim.Designer.sendMessage("LoadLevel", "interior,walk");
        SetAppStep("walkthough");
        return false;
    });


}

function SetAppStep(step) {
    $("body").removeClass("current-exterior current-furniture current-wallpaper current-walkthough");
    $("body").addClass("current-" + step);
    $(".menu.steps > li").removeClass("selected");
    $(".menu.steps a.step-" + step).closest("li").addClass("selected");
}

function DebugLog(message) {
    try {
        console.log(message);
    }catch(ex) {
    }
}

var designer = SR.Heim.Designer.init('#designer', {
    onUnityLoaded: function () {
		//designer.loadHouse();
		//designer.loadAssetBundle($currentProject.ModelFilePath);
        SR.Heim.Designer.sendMessage("SetDebugUI", "false");
        initNavigators();
        $(".step-exterior").click();

		$('.toolbox li').click(function () {
			var $this = $(this);
			var $parent = $this.parent();
			var type = $parent.attr('data-type');

			$parent.find('.selected').removeClass('selected');
			$this.addClass('selected');

			var func = $this.parent("ul").data("func");
			var cast = $this.parent("ul").data("cast");
			var id = $this.data("id");
			if (cast == "int") {
			    id = parseInt(id);
			}
			SR.Heim.Designer.sendMessage(func, id);

            /*
			var mapping = JSON.parse($this.attr('data-mapping'));
			
			switch (type) {
				case 'RoofTile': {
					designer.setRoof(mapping.materialId);
					break;
				}

				case 'Tile': {
					var floor = $this.closest('.floors').attr('data-floor');
					designer.setFloor(mapping.materialId, floor);
					break;
				}
			}
            */
		});
	}
});