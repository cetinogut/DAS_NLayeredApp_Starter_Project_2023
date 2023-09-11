using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayered.Core.DTOs
{
    public class CustomResponseDto<T> //API responsetur
    {
        public T Data { get; set; }

        [JsonIgnore] //clientlerin bunu görmesine gerek yok, onlar zaten alıyorlar
        public int StatusCode { get; set; }

        public List<String> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data) //Static Factory Design PAttern örneği static method ile sınıfın instansı dönüyor
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }

    }

    public class CustomNoContentResponseDto //API responsetur, bunu da DATA olmayan responselarda NoContentDto yerine dönebiliriz.
    {

        [JsonIgnore] //clientlerin bunu görmesine gerek yok, onlar zaten alıyorlar
        public int StatusCode { get; set; }

        public List<String> Errors { get; set; }

        public static CustomNoContentResponseDto Success(int statusCode) //Static Factory Design PAttern örneği static method ile sınıfın instansı dönüyor
        {
            return new CustomNoContentResponseDto {StatusCode = statusCode };
        }
       
        public static CustomNoContentResponseDto Fail(int statusCode, List<string> errors)
        {
            return new CustomNoContentResponseDto { StatusCode = statusCode, Errors = errors };
        }

        public static CustomNoContentResponseDto Fail(int statusCode, string error)
        {
            return new CustomNoContentResponseDto { StatusCode = statusCode, Errors = new List<string> { error } };
        }

    }
}
