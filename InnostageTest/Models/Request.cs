using System;
using System.ComponentModel.DataAnnotations;

namespace InnostageTest.Models
{
    public class Request
    {
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Не указано ФИО клиента")]
        [Display(Name = "ФИО клиента")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Не указан телефонный номер клиента")]
        [Phone(ErrorMessage = "Некорретный номер телефона")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан текст обращения")]
        [Display(Name = "Текст обращения")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Не указано время обращения")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy. HH:mm}")]
        [Display(Name = "Время обращения")]
        public DateTime RequestTime { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public User Creator { get; set; }
    }
}
