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
        protected DotNetObjectReference<NameOfComponent> Self
        {
            get => self ?? (Self = DotNetObjectReference.Create(this));
            set => self = value;
        }

        protected ElementReference ElemRef { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var options = await this.GetState<NameOfComponent, Options>();

            if (firstRender)
            {
                if (options is null)
                {
                    options = this.GetOptions();
                }
                else
                {
                    await this.CheckState(options);
                }

                initialized = true;
            }
            else
            {
                // Use current state if...
                if (this.initialized || options is null)
                {
                    options = this.GetOptions();
                }
            }
            
            await this.Save<NameOfComponent, Options>(options);
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
                new {
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
                new {
                    this.ElemRef
                },
                options);

            await this.Save<NameOfComponent, Options>(options);
        }

        protected Options GetOptions()
        {
            var options = new Options 
            {
                
            };

            base.SetOptions(options);

            return options;
        }

        protected async Task CheckState(Options options)
        {
            bool stateChanged = false;
            

            if (await base.CheckState(options) || stateChanged)
                StateHasChanged();
        }

        public override void Dispose()
        {
            self?.Dispose();
            base.Dispose();
        }
    }
}