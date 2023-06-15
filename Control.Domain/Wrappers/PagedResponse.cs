﻿using Control.Domain.Settings;

namespace Control.Domain.Wrappers
{
    public class PagedResponse<T>
    {
        public PagedResponse(T data)
        {
            Data = data;
            Succeeded = true;
            Message = string.Empty;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public MetadataSetting Meta { get; set; }
    }
}