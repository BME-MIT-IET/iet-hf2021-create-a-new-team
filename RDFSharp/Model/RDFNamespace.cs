/*
   Copyright 2012-2020 Marco De Salvo

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Text.RegularExpressions;

namespace RDFSharp.Model
{

    /// <summary>
    /// RDFNamespace represents a generic namespace in the RDF model.
    /// </summary>
    public class RDFNamespace : IEquatable<RDFNamespace>
    {

        #region Properties
        /// <summary>
        /// Unique representation of the namespace
        /// </summary>
        internal long NamespaceID { get; set; }

        /// <summary>
        /// Flag indicating that the namespace is temporary
        /// </summary>
        internal bool IsTemporary { get; set; }

        /// <summary>
        /// Prefix representation of the namespace
        /// </summary>
        public string NamespacePrefix { get; internal set; }

        /// <summary>
        /// Uri representation of the namespace
        /// </summary>
        public Uri NamespaceUri { get; internal set; }

        /// <summary>
        /// Uri dereference representation of the namespace
        /// </summary>
        public Uri DereferenceUri { get; internal set; }
        #endregion

        #region Ctors
        /// <summary>
        /// Builds a namespace with given prefix and Uri
        /// </summary>
        public RDFNamespace(string prefix, string uri)
        {

            //Validate prefix: must contain only letters/numbers and cannot be "bnode" or "xmlns"
            if (prefix != null && prefix.Trim() != string.Empty)
            {
                prefix = prefix.Trim();

                if (Regex.IsMatch(prefix, @"^[a-zA-Z0-9_\-]+$"))
                {
                    if (prefix.ToUpperInvariant() == "BNODE" || prefix.ToUpperInvariant() == "XMLNS")
                    {
                        throw new RDFModelException("Cannot create RDFNamespace because \"prefix\" parameter cannot be \"bnode\" or \"xmlns\"");
                    }
                }
                else
                {
                    throw new RDFModelException("Cannot create RDFNamespace because \"prefix\" parameter contains unallowed characters");
                }

            }
            else
            {
                throw new RDFModelException("Cannot create RDFNamespace because \"prefix\" parameter is null or empty");
            }

            //Validate uri: must be an absolute Uri and cannot start with "bnode:" or "xmlns:"
            if (uri != null && uri.Trim() != string.Empty)
            {
                uri = uri.Trim();

                Uri _uri = RDFModelUtilities.GetUriFromString(uri);
                if (_uri != null)
                {
                    if (!_uri.ToString().ToUpperInvariant().StartsWith("BNODE:") && !_uri.ToString().ToUpperInvariant().StartsWith("XMLNS:"))
                    {
                        this.NamespacePrefix = prefix;
                        this.NamespaceUri = _uri;
                        this.DereferenceUri = _uri;
                        this.NamespaceID = RDFModelUtilities.CreateHash(this.ToString());
                    }
                    else
                    {
                        throw new RDFModelException("Cannot create RDFNamespace because \"uri\" parameter cannot start with \"bnode:\" or \"xmlns:\"");
                    }
                }
                else
                {
                    throw new RDFModelException("Cannot create RDFNamespace because \"uri\" parameter is not a valid Uri");
                }

            }
            else
            {
                throw new RDFModelException("Cannot create RDFNamespace because \"uri\" parameter is null or empty");
            }

        }
        #endregion

        #region Interfaces
        /// <summary>
        /// Gives the string representation of the namespace
        /// </summary>
        public override string ToString()
        {
            return this.NamespaceUri.ToString();
        }

        /// <summary>
        /// Performs the equality comparison between two namespaces
        /// </summary>
        public bool Equals(RDFNamespace other)
        {
            return (other != null && this.NamespaceID.Equals(other.NamespaceID));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the Uri for dereferencing the namespace as an RDF representation
        /// </summary>
        public RDFNamespace SetDereferenceUri(Uri dereferenceUri)
        {
            if (dereferenceUri != null &&
                    dereferenceUri.IsAbsoluteUri &&
                        !dereferenceUri.ToString().ToUpperInvariant().StartsWith("BNODE:") &&
                            !dereferenceUri.ToString().ToUpperInvariant().StartsWith("XMLNS:"))
            {
                this.DereferenceUri = dereferenceUri;
            }
            return this;
        }

        /// <summary>
        /// Sets or unsets this namespace as temporary
        /// </summary>
        internal RDFNamespace SetTemporary(bool temporary)
        {
            this.IsTemporary = temporary;
            return this;
        }
        #endregion

    }

}