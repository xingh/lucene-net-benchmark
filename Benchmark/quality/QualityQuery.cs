﻿/**
 * Copyright 2005 The Apache Software Foundation
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Specialized;

namespace Lucene.Net.Benchmark.quality {

	/**
	 * A QualityQuery has an ID and some name-value pairs.
	 * <p> 
	 * The ID allows to map the quality query with its judgements.
	 * <p>
	 * The name-value pairs are used by a 
	 * {@link org.apache.lucene.benchmark.quality.QualityQueryParser}
	 * to create a Lucene {@link org.apache.lucene.search.Query}.
	 * <p>
	 * It is very likely that name-value-pairs would be mapped into fields in a Lucene query,
	 * but it is up to the QualityQueryParser how to map - e.g. all values in a single field, 
	 * or each pair as its own field, etc., - and this of course must match the way the 
	 * searched index was constructed.
	 */
	public class QualityQuery : IComparable {

		private String queryID;
		private StringDictionary nameValPairs;

		/**
		 * Create a QualityQuery with given ID and name-value pairs.
		 * @param queryID ID of this quality query.
		 * @param nameValPairs the contents of this quality query.
		 */
		public QualityQuery( String queryID, StringDictionary nameValPairs ) {
			this.queryID = queryID;
			this.nameValPairs = nameValPairs;
		}

		/**
		 * Return all the names of name-value-pairs in this QualityQuery.
		 */
		public String[] getNames() {
			return (String[])nameValPairs.Keys;
		}

		/**
		 * Return the value of a certain name-value pair.
		 * @param name the name whose value should be returned. 
		 */
		public String getValue( String name ) {
			return (String)nameValPairs[ name ];
		}

		/**
		 * Return the ID of this query.
		 * The ID allows to map the quality query with its judgements.
		 */
		public String getQueryID() {
			return queryID;
		}

		/* for a nicer sort of input queries before running them.
		 * Try first as ints, fall back to string if not int. */
		int IComparable.CompareTo( object o ) {
			QualityQuery other = (QualityQuery)o;
			try {
				// compare as ints when ids ints
				int n = int.Parse( queryID );
				int nOther = int.Parse( other.queryID );
				return n - nOther;
			}
			catch( FormatException ) {
				// fall back to string comparison
				return queryID.CompareTo( other.queryID );
			}
		}
	}
}
