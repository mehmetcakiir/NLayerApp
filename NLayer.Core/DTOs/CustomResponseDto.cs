using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        // Clinent a gösterilmeyeceği için json formatına dönüştürülmeyecek
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }


        // Başarılı - Data ve Status Code 
        public static CustomResponseDto<T> Succes(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }

        // Başarılı - Status Code
        public static CustomResponseDto<T> Succes(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        // Başarısız - Status Code ve Errors(1 den fazla hata)
        public static CustomResponseDto<T> Fail(int statusCode, List<String> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        // Başarısız - Status Code ve Errors
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error} };
        }
    }
}
