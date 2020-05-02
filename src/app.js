// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

if (!window.Mobsites) {
	window.Mobsites = {
		Blazor: {

		}
	};
}

window.Mobsites.Blazor.NameOfComponent = {
    init: function (instance, elemRefs, options) {
        this.instance = instance;
        this.elemRefs = elemRefs;
        this.options = options;

        // Add initialization logic
        this.initialized = true;

        return this.initialized;
    },
    refresh: function (instance, elemRefs, options) {
        // Add refresh logic if any
        return this.init(instance, elemRefs, options);
    }
}