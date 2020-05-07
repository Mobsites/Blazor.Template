// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

// import statements if any

if (!window.Mobsites) {
	window.Mobsites = {
		Blazor: {

		}
	};
}

window.Mobsites.Blazor.NameOfComponents = {
    store: [],
    init: function (dotNetObjRef, elemRefs, options) {
        try {
            const index = this.add(new Mobsites_Blazor_NameOfComponent(dotNetObjRef, elemRefs, options));
            dotNetObjRef.invokeMethodAsync('SetIndex', index);
            return true;
        } catch (error) {
            console.log(error);
            return false;
        }
    },
    add: function (nameOfComponent) {
        for (let i = 0; i < this.store.length; i++) {
            if (this.store[i] == null)
            {
                this.store[i] = nameOfComponent;
                return i;
            }
        }
        const index = this.store.length;
        this.store[index] = nameOfComponent;
        return index;
    },
    update: function (index, options) {
        this.store[index].update(options);
    },
    destroy: function (index) {
        this.store[index] = null;
    }
}

class Mobsites_Blazor_NameOfComponent /*extends SomeClass*/ {
    constructor(dotNetObjRef, elemRefs, options) {
        //super();
        this.dotNetObjRef = dotNetObjRef;
        this.elemRefs = elemRefs;
        this.dotNetObjOptions = options;
        // Any other set up code can go here as well.
    }
    update(options) {
        this.dotNetObjOptions = options;
    }
}