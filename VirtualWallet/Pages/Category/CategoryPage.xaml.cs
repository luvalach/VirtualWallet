﻿using BL.Models;
using BL.Service;
using VirtualWallet.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Pages
{
    public sealed partial class CategoryPage : Page
    {
        private CategoryPageViewModel viewModel;

        public CategoryPage()
        {
            this.InitializeComponent();
            viewModel = new CategoryPageViewModel(new CategoryService(), new WalletService(), new WalletCategoryService(), new CategoryRuleService());
            this.DataContext = viewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var pagePayload = (PagePayload)e.Parameter;
            viewModel.Category = (Category)pagePayload.Dto;
            await viewModel.LoadDataAsync();

            if (pagePayload.NewImage != null)
            {
                viewModel.Image = pagePayload.NewImage;
            }

            if (pagePayload.NewRule != null)
            {
                viewModel.Rules.Add(pagePayload.NewRule);
            }

            base.OnNavigatedTo(e);
        }

        private void GridViewCategories_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void GridViewWallets_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ListViewRules_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void SaveAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.SaveCategoryAsync();
        }

        private async void CancelAppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await viewModel.DiscardChangesAsync();
        }

        private void IconButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ImagesPage), viewModel.Image);
        }

        private void AddRuleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RulePage));
        }
    }
}
