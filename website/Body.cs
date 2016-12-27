﻿using System;
using System.Linq;

using EntryPoint;

namespace Website {

    class Body {
        /// ## About EntryPoint
        /// 
        /// An argument parser with an API intentionally designed to be composable and maintainable.
        ///
        /// Supports:
        /// 
        /// * .Net Standard 1.6+ (All future.Net releases are built on this)
        /// * .Net Framework 4.5.1+
        ///
        /// Follows the [IEEE Standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) 
        /// closely, but does include common adblibs such as fully named --option style options.
        ///
        /// ## Installation
        /// EntryPoint is available on [NuGet](https://www.nuget.org/packages/LimeBean):
        /// 
        ///     PM> Install-Package EntryPoint
        ///     
    }

    /// ## Basic Usage
    /// EntryPoint is designed so you can write declarative `ApplicationOptions` 
    /// classes and then simply pass to EntryPoint for population
    /// 
    /// Let's say we want a utility used like: `UtilityName -s --name Bob 6.1`
    /// 
    /// This has one Option, one OptionParameter and a positional Operand
#if CODE
    class SimpleApplicationOptions : BaseApplicationOptions {
        public SimpleApplicationOptions() : base("SimpleApp") { }

        // Option
        [Option(
            LongName = "switch", ShortName = 's')]
        public bool Switch { get; set; }

        // Option-Parameter
        [OptionParameter(
            LongName = "name", ShortName = 'n')]
        public string Name { get; set; }

        // Operand
        [Operand(position: 1)]
        public decimal FirstOperand { get; set; }
    }

    class SimpleProgram {
        void main(string[] args) {
            
            // One line parsing of any BaseApplicationOptions implementation
            var options = EntryPointApi.Parse<SimpleApplicationOptions>(args);

            // Object oriented access to your arguments
            Console.WriteLine($"The name is {options.Name}");
            Console.WriteLine($"Switch flag: {options.Switch}");
            Console.WriteLine($"Positional Operand 1: {options.FirstOperand}");
        }
    }
#endif

    /// ## Attributes
    /// 
    /// `BaseApplicationOptions` implementations use Attributes to define CLI functionality
    /// 
    /// #### `[Option(LongName = string, ShortName = char)]`
    /// * **Apply to:** Class Properties
    /// * **Output Types:** Bool
    /// * **Detail:** Defines an On/Off option for use on the CLI
    /// * **Argument, LongName:** the case in-sensitive name to be used like `--name`
    /// * **Argument, ShortName:** the case sensitive character to be used like `-n`
    /// 
    /// #### `[OptionParameter(LongName = string, ShortName = char)]`
    /// * **Apply to:** Class Properties
    /// * **Output Types:** Primitive Types, Enums
    /// * **Detail:** Defines a parameter which can be invoked to provide a value
    /// * **Argument, LongName:** the case in-sensitive name to be used like `--name`
    /// * **Argument, ShortName:** the case sensitive character to be used like `-n`
    /// 
    /// #### `[Operand(position = int)]`
    /// * **Apply to:** Class Properties
    /// * **Output Types:** Primitive Types, Enums
    /// * **Detail:** Maps a positional operand from the end of a CLI command
    /// * **Argument, LongName:** the case in-sensitive name to be used like `--name`
    /// * **Argument, ShortName:** the case sensitive character to be used like `-n`
    /// 
    /// #### `[Required]`
    /// * **Apply to:** Class Properties with any Option or Operand Attribute applied
    /// * **Detail:** Makes an Option or Operand mandatory for the user to provide
    /// 
    /// #### `[Help(detail = string)]`
    /// * **Apply to:** Class Properties with any Option or Operand Attribute applied, or an ApplicationOptions Class
    /// * **Detail:** Provides custom documentation on an Option, Operand or ApplicationOptions Class, which is consumed by the --help generator
    ///
    /// ## Messaging Application
    /// 
    /// The following is an example implementation for use in a simple message sending application
    /// 
    /// This is used like `UtilityName [ -v | --verbose ] [ -s | --subject "your subject" ] [ -i | --importance [ 1 | normal | 2 | high ] ] [message]`
#if CODE
    class MessagingApplicationOptions : BaseApplicationOptions {
        public MessagingApplicationOptions() : base("Message Sender") { }

        // A verbose Option with help documentation
        [Option(LongName = "verbose", 
                ShortName = 'v')]
        [Help("When this is set, verbose logging will be activated")]
        public bool Verbose { get; set; }

        // A message which *must* be provided, 
        [Required]
        [OptionParameter(LongName = "subject",
                         ShortName = 's')]
        [Help("Mandatory Subject to provide")]
        public string Subject { get; set; }

        // An importance level for the message
        // If not provided is defaulted to `Normal`
        // User can provide the value as a number or string (ie. '2' or 'high')
        [OptionParameter(LongName = "importance", 
                         ShortName = 'i')]
        [Help("Sets the importance level of a sent message")]
        public MessageImportanceEnum Importance { get; set; } = MessageImportanceEnum.Normal;

        // A message which *must* be provided, 
        [Required]
        [Operand(1)]
        [Help("Mandatory message to provide")]
        public string Message { get; set; }
    }

    // Usage is then as simple as
    class MessagingProgram {
        void main(string[] args) {
            var options = EntryPointApi.Parse<MessagingApplicationOptions>(args);

            // Use the options object...
        }
    }

    enum MessageImportanceEnum {
        Normal = 1,
        High = 2
    }
#endif
}
