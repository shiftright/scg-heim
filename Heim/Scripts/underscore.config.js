if (typeof _ != 'undefined') {
	_.templateSettings = {
		evaluate: /\{\{(.+?)\}\}/g,
		interpolate: /\{\{=(.+?)\}\}/g,
		escape: /\{\{#(.+?)\}\}/g
	};

	var __template = { };
	_.findTemplate = function (name) {
		if (typeof __template[name] == 'undefined') {
			__template[name] = $('[name=' + name + ']').html();
		}

		return __template[name];
	}
}