﻿using System;

namespace MiniAccounting.Infrastructure.Logger
{
    public class PrefixLogger : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _prefix;

        public LogLevel LogLevel { get; set; }

        public PrefixLogger(ILogger logger, string prefix)
        {
            if (logger is PrefixLogger prefixLogger)
                _logger = prefixLogger._logger;
            else
                _logger = logger;

            _prefix = prefix;
        }

        public void Error(Exception exception)
        {
            _logger.Error($"{_prefix}{exception}");
        }

        public void Write(LogLevel logLevel, string msg)
        {
            _logger.Write(logLevel, $"{_prefix}{msg}");
        }
    }
}
