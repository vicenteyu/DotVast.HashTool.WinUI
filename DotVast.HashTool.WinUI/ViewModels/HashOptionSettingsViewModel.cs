using System.Collections.ObjectModel;
using System.Diagnostics;

using CommunityToolkit.Mvvm.Messaging.Messages;

using DotVast.HashTool.WinUI.Contracts.Services;
using DotVast.HashTool.WinUI.Contracts.ViewModels;
using DotVast.HashTool.WinUI.Models;

namespace DotVast.HashTool.WinUI.ViewModels;

public partial class HashOptionSettingsViewModel : ObservableRecipient, INavigationAware
{
    private readonly IHashOptionsService _hashOptionsService;

    public HashOptionSettingsViewModel(IHashOptionsService hashOptionsService)
    {
        _hashOptionsService = hashOptionsService;
    }

    public ObservableCollection<HashOption> HashOptions => _hashOptionsService.HashOptions;

    #region Messenger

    protected override void OnActivated()
    {
        // PropertyChangedMessage[HashOption.IsEnabled]
        Messenger.Register<HashOptionSettingsViewModel, PropertyChangedMessage<bool>>(this, async (r, m) =>
        {
            if (m.Sender is HashOption hashOption && m.PropertyName == nameof(HashOption.IsEnabled))
            {
                Debug.WriteLine($"---------------- {DateTime.Now} -- HashOptionSettingsViewModel.Messenger.PropertyChangedMessage[HashOption.IsEnabled]");
                Debug.WriteLine($"Hash.Name: {hashOption.Hash.Name}");
                Debug.WriteLine($"IsEnabled:{hashOption.IsEnabled}");

                await _hashOptionsService.SetHashOptionsAsync(HashOptions);
            }
        });
    }

    #endregion Messenger

    #region INavigationAware

    public void OnNavigatedTo(object? parameter)
    {
        IsActive = true;
        HashOptions.CollectionChanged += HashOptions_CollectionChanged_Reorder;
    }

    public void OnNavigatedFrom()
    {
        IsActive = false;
        HashOptions.CollectionChanged -= HashOptions_CollectionChanged_Reorder;
    }

    #endregion INavigationAware

    private void HashOptions_CollectionChanged_Reorder(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (sender is ObservableCollection<HashOption> hashOptions
            && e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        {
            _hashOptionsService.SetHashOptionsAsync(hashOptions);
        }
    }
}