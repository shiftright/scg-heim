
var DEBUG = true;

var designer = SR.Heim.Designer.init('#designer', {
	onUnityLoaded: function () {
		//designer.loadHouse();
		designer.loadAssetBundle($currentProject.ModelFilePath);

		$('.toolbox li').click(function () {
			var $this = $(this);
			var $parent = $this.parent();
			var type = $parent.attr('data-type');

			$parent.find('.selected').removeClass('selected');
			$this.addClass('selected');

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
		});
	}
});