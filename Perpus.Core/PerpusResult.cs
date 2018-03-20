using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Core
{
    public class PerpusResult
    {
        private static readonly PerpusResult _success = new PerpusResult(true);

        /// <summary>
        /// True if the operation was successful
        /// 
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// List of errors
        /// 
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// Static success result
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static PerpusResult Success
        {
            get
            {
                return PerpusResult._success;
            }
        }

        static PerpusResult()
        {
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// 
        /// </summary>
        /// <param name="errors"/>
        public PerpusResult(params string[] errors)
            : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// 
        /// </summary>
        /// <param name="errors"/>
        public PerpusResult(IEnumerable<string> errors)
        {
            //    if (errors == null)
            //        errors = (IEnumerable<string>)new string[1]
            //{
            //  //Resources.DefaultError
            //};
            this.Succeeded = false;
            this.Errors = errors;
        }

        /// <summary>
        /// Constructor that takes whether the result is successful
        /// 
        /// </summary>
        /// <param name="success"/>
        protected PerpusResult(bool success)
        {
            this.Succeeded = success;
            this.Errors = (IEnumerable<string>)new string[0];
        }

        /// <summary>
        /// Failed helper method to generate error message
        /// based on validation custom message
        /// </summary>
        /// <param name="errorMessages"/>
        /// <returns/>
        public static PerpusResult Failed(params string[] errorMessages)
        {
            return new PerpusResult(errorMessages);
        }

        /// <summary>
        /// Failed helper method to generate error message
        /// based on validation custom message
        /// </summary>
        /// <param name="errorMessages"/>
        /// <returns/>
        public static PerpusResult Failed(IEnumerable<string> errorMessages)
        {
            return new PerpusResult(errorMessages);
        }

        /// <summary>
        /// Failed helper method to generate error message triggered on exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static PerpusResult Failed(Exception ex)
        {
            return new PerpusResult(getInnerException(ex));
        }

        /// <summary>
        /// Function to generate the inner exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string getInnerException(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex.Message;
            }
            else
            {
                return getInnerException(ex.InnerException);
            }
        }
    }
}
