// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI component for rendering an icon.
    /// </summary>
    public partial class NameOfComponent
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Clear all state for this UI component and any of its dependents from browser storage.
        /// </summary>
        public ValueTask ClearState() => this.ClearState<NameOfComponent, Options>();



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        private DotNetObjectReference<NameOfComponent> self;

        /// <summary>
        /// Net reference passed into javascript representation.
        /// </summary>
        protected DotNetObjectReference<NameOfComponent> Self
        {
            get => self ?? (Self = DotNetObjectReference.Create(this));
            set => self = value;
        }

        /// <summary>
        /// Dom element reference passed into javascript representation.
        /// </summary>
        protected ElementReference ElemRef { get; set; }

        /// <summary>
        /// Life cycle method for when component has been rendered in the dom and javascript interopt is fully ready.
        /// </summary>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Initialize();
            }
            else
            {
                await Refresh();
            }
        }

        private async Task Initialize()
        {
            var options = await this.GetState<NameOfComponent, Options>();

            if (options is null)
            {
                options = this.GetOptions();
            }
            else
            {
                await this.CheckState(options);
            }

            // Destroy any lingering js representation.
            options.Destroy = true;

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.NameOfComponent.init",
                Self,
                new
                {
                    this.ElemRef
                },
                options);

            await this.Save<NameOfComponent, Options>(options);
        }

        private async Task Refresh()
        {
            var options = await this.GetState<NameOfComponent, Options>();

            // Use current state if...
            if (this.initialized || options is null)
            {
                options = this.GetOptions();
            }

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.NameOfComponent.refresh",
                Self,
                new
                {
                    this.ElemRef
                },
                options);

            await this.Save<NameOfComponent, Options>(options);
        }

        /// <summary>
        /// Get current or storage-saved options for keeping state.
        /// </summary>

        protected Options GetOptions()
        {
            var options = new Options
            {

            };

            base.SetOptions(options);

            return options;
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
        protected async Task CheckState(Options options)
        {
            bool stateChanged = false;


            if (await base.CheckState(options) || stateChanged)
                StateHasChanged();
        }

        /// <summary>
        /// Called by GC.
        /// </summary>
        public override void Dispose()
        {
            self?.Dispose();
            base.Dispose();
        }
    }
}