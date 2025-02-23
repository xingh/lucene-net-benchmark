﻿/**
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
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
using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Benchmark.ByTask.Feeds;
using Lucene.Net.Documents;
using Lucene.Net.Highlight;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace Lucene.Net.Benchmark.ByTask.Tasks {

	/**
	 * Read index (abstract) task.
	 * Sub classes implement withSearch(), withWarm(), withTraverse() and withRetrieve()
	 * methods to configure the actual action.
	 * <p/>
	 * <p>Note: All ReadTasks reuse the reader if it is already open.
	 * Otherwise a reader is opened at start and closed at the end.
	 * <p>
	 * The <code>search.num.hits</code> config parameter sets
	 * the top number of hits to collect during searching.
	 * <p>Other side effects: none.
	 */
	public abstract class ReadTask : PerfTask {

		public ReadTask(PerfRunData runData) : base(runData) {
			// just run base constructor
		}

		public override int doLogic() {
			int res = 0;
			bool closeReader = false;

			// open reader or use existing one
			IndexReader ir = getRunData().getIndexReader();
			if (ir == null) {
				Directory dir = getRunData().getDirectory();
				ir = IndexReader.Open(dir);
				closeReader = true;
				//res++; //this is confusing, comment it out
			}

			// optionally warm and add num docs traversed to count
			if (withWarm()) {
				Document doc = null;
				for (int m = 0; m < ir.MaxDoc(); m++) {
					if (!ir.IsDeleted(m)) {
						doc = ir.Document(m);
						res += (doc == null ? 0 : 1);
					}
				}
			}

			if (withSearch()) {
				res++;
				IndexSearcher searcher;
				if (closeReader) {
					searcher = new IndexSearcher(ir);
				} else {
					searcher = getRunData().getIndexSearcher();
				}
				QueryMaker queryMaker = getQueryMaker();
				Query q = queryMaker.makeQuery();
				Sort sort = this.getSort();
				TopDocs hits;
				int _numHits = numHits();
				if (_numHits > 0) {
					if (sort != null) {
						// TODO: change the following to create TFC with in/out-of order
						// according to whether the query's Scorer.
						TopFieldCollector collector = TopFieldCollector.create(sort, _numHits,
							true, withScore(), withMaxScore(), false);
						searcher.Search(q, collector);
						hits = collector.TopDocs();
					} else {
						hits = searcher.Search(q, _numHits);
					}
					Benchmark.LogSheet.AddRowAndCell( "q=" + q + ":" + hits.totalHits + " total hits" ); 
					Console.WriteLine("q=" + q + ":" + hits.totalHits + " total hits"); 

					if (withTraverse()) {
						ScoreDoc[] scoreDocs = hits.scoreDocs;
						int ts = Math.Min(scoreDocs.Length, traversalSize());

						if (ts > 0) {
							bool retrieve = withRetrieve();
							int numHighlight = Math.Min(numToHighlight(), scoreDocs.Length);
							Analyzer analyzer = getRunData().getAnalyzer();
							BenchmarkHighlighter highlighter = null;
							if (numHighlight > 0) {
								highlighter = getBenchmarkHighlighter(q);
							}
							for (int m = 0; m < ts; m++) {
								int id = scoreDocs[m].doc;
								res++;
								if (retrieve) {
									Document document = retrieveDoc(ir, id);
									res += document != null ? 1 : 0;
									if (numHighlight > 0 && m < numHighlight) {
										List<string> fieldsToHighlight = getFieldsToHighlight(document);
										foreach (string field in fieldsToHighlight) {
											String text = document.Get(field);
											res += highlighter.doHighlight(ir, id, field, document, analyzer, text);
										}
									}
								}
							}
						}
					}
				}
				searcher.Close();
			}

			if (closeReader) {
				ir.Close();
			}
			return res;
		}



		public virtual Document retrieveDoc(IndexReader ir, int id) {
			return ir.Document(id);
			//  throws IOException
		}

		/**
		* Return query maker used for this task.
		*/
		public abstract QueryMaker getQueryMaker();

		/**
		* Return true if search should be performed.
		*/
		public abstract bool withSearch();
  

		/**
		* Return true if warming should be performed.
		*/
		public abstract bool withWarm();

		/**
		* Return true if, with search, results should be traversed.
		*/
		public abstract bool withTraverse();

		/** Whether scores should be computed (only useful with
		*  field sort) */
		public virtual bool withScore() {
			return true;
		}

		/** Whether maxScores should be computed (only useful with
		*  field sort) */
		public virtual bool withMaxScore() {
			return true;
		}

		/**
		* Specify the number of hits to traverse.  Tasks should override this if they want to restrict the number
		* of hits that are traversed when {@link #withTraverse()} is true. Must be greater than 0.
		* <p/>
		* Read task calculates the traversal as: Math.min(hits.length(), traversalSize())
		*
		* @return Integer.MAX_VALUE
		*/
		public virtual int traversalSize() {
			return int.MaxValue;
		}

		const int DEFAULT_SEARCH_NUM_HITS = 10;
		private int _numHits;

		public override void setup() {
			base.setup();
			_numHits = getRunData().getConfig().get("search.num.hits", DEFAULT_SEARCH_NUM_HITS);
		}

		/**
		* Specify the number of hits to retrieve.  Tasks should override this if they want to restrict the number
		* of hits that are collected during searching. Must be greater than 0.
		*
		* @return 10 by default, or search.num.hits config if set.
		*/
		public virtual int numHits() {
			return _numHits;
		}

		/**
		* Return true if, with search & results traversing, docs should be retrieved.
		*/
		public abstract bool withRetrieve();

		/**
		* Set to the number of documents to highlight.
		*
		* @return The number of the results to highlight.  O means no docs will be highlighted.
		*/
		public virtual int numToHighlight() {
			return 0;
		}

		/**
		* @deprecated Use {@link #getBenchmarkHighlighter(Query)}
		*/
		public virtual Highlighter getHighlighter(Query q) {
			// not called
			return null;
		}
  
		/**
		* Return an appropriate highlighter to be used with
		* highlighting tasks
		*/
		protected virtual BenchmarkHighlighter getBenchmarkHighlighter(Query q){
			return null;
		}

		/**
		* @return the maximum number of highlighter fragments
		* @deprecated Please define getBenchmarkHighlighter instead
		*/
		public virtual int maxNumFragments(){
			// not called -- we switched this method to final to
			// force any external subclasses to cutover to
			// getBenchmarkHighlighter instead
			return 10;
		}

		/**
		*
		* @return true if the highlighter should merge contiguous fragments
		* @deprecated Please define getBenchmarkHighlighter instead
		*/
		public virtual bool isMergeContiguousFragments(){
			// not called -- we switched this method to final to
			// force any external subclasses to cutover to
			// getBenchmarkHighlighter instead
			return false;
		}

		/**
		* @deprecated Please define getBenchmarkHighlighter instead
		*/
		public virtual int doHighlight(TokenStream ts, String text,  Highlighter highlighter, bool mergeContiguous, int maxFragments) {
			// not called -- we switched this method to final to
			// force any external subclasses to cutover to
			// getBenchmarkHighlighter instead
			return 0;
		}
  
		public virtual Sort getSort() {
			return null;
		}

		/**
		* Define the fields to highlight.  Base implementation returns all fields
		* @param document The Document
		* @return A Collection of Field names (Strings)
		*/
		public virtual List<string> getFieldsToHighlight(Document document) {
			List<string> result = new List<string>();
			// TODO: there's probably a better way to do this, especially
			// in LINQ, but this will do for now.
			List<Field> Fields = new List<Field>();
			foreach( Field field in document.GetFields() ) {
				Fields.Add( field );
			}
			foreach (Field Field in Fields)
			{
				result.Add(Field.Name());
			}
			return result;
		}
	}
}
