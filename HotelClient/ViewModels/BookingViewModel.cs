using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using HotelClient.Models;
using HotelClient.Services;

namespace HotelClient.ViewModels
{
    public class BookingViewModel : BindableObject
    {
        private readonly BookingService bookingService;

        public BookingViewModel(Hotel selectedHotel, BookingService bookingService)
        {
            SelectedHotel = selectedHotel ?? throw new ArgumentNullException(nameof(selectedHotel));
            this.bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            SubmitBookingCommand = new Command(async () => await OnSubmitBookingAsync());
        }

        public Hotel SelectedHotel { get; }

        private string guestName;
        public string GuestName
        {
            get => guestName;
            set
            {
                if (guestName != value)
                {
                    guestName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string guestEmail;
        public string GuestEmail
        {
            get => guestEmail;
            set
            {
                if (guestEmail != value)
                {
                    guestEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime checkInDate = DateTime.Today;
        public DateTime CheckInDate
        {
            get => checkInDate;
            set
            {
                if (checkInDate != value)
                {
                    checkInDate = value;
                    if (CheckOutDate <= checkInDate)
                        CheckOutDate = checkInDate.AddDays(1);
                    OnPropertyChanged();
                }
            }
        }

        private DateTime checkOutDate = DateTime.Today.AddDays(1);
        public DateTime CheckOutDate
        {
            get => checkOutDate;
            set
            {
                if (checkOutDate != value)
                {
                    checkOutDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int numberOfGuests = 1;
        public int NumberOfGuests
        {
            get => numberOfGuests;
            set
            {
                if (numberOfGuests != value)
                {
                    numberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SubmitBookingCommand { get; }

        private async System.Threading.Tasks.Task OnSubmitBookingAsync()
        {
            if (string.IsNullOrWhiteSpace(GuestName) || string.IsNullOrWhiteSpace(GuestEmail))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Пожалуйста, заполните имя и email.", "OK");
                return;
            }

            if (CheckOutDate <= CheckInDate)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Дата выезда должна быть позже даты заезда.", "OK");
                return;
            }

            var bookingRequest = new BookingRequest
            {
                HotelId = SelectedHotel.Id.ToString(),
                GuestName = GuestName,
                GuestEmail = GuestEmail,
                CheckInDate = CheckInDate,
                CheckOutDate = CheckOutDate,
                NumberOfGuests = NumberOfGuests
            };

            try
            {
                bool success = await bookingService.CreateBookingAsync(bookingRequest);
                if (success)
                {
                    await Application.Current.MainPage.DisplayAlert("Успех", "Запрос на бронирование отправлен! Ожидайте письмо на почту.", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось сохранить бронирование.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка при отправке бронирования: {ex.Message}", "OK");
            }
        }
    }
}
