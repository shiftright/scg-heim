
var DEBUG = true;

var designer = SR.Heim.Designer.init('#designer', {
	loader: $currentProject.ModelFilePath,
	onUnityLoaded: function () {
		//designer.loadAssetBundle();
		designer.loadHouse();

		$('.toolbox li').click(function () {
			var $this = $(this);
			var $parent = $this.parent();
			var type = $parent.attr('data-type');

			$parent.find('.selected').removeClass('selected');
			$this.addClass('selected');

			//var args = {
			//	name: '',
			//	file: $this.attr('data-asset')
			//}

			switch (type) {
				case 'RoofTile': {
					designer.setRoof($this.attr('data-asset'));
					break;
				}
			}
		});
	}
});