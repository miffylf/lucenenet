﻿using System.Collections.Generic;

namespace org.apache.lucene.analysis.pattern
{

	/*
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


	using CharFilterFactory = org.apache.lucene.analysis.util.CharFilterFactory;

	/// <summary>
	/// Factory for <seealso cref="PatternReplaceCharFilter"/>. 
	/// <pre class="prettyprint">
	/// &lt;fieldType name="text_ptnreplace" class="solr.TextField" positionIncrementGap="100"&gt;
	///   &lt;analyzer&gt;
	///     &lt;charFilter class="solr.PatternReplaceCharFilterFactory" 
	///                    pattern="([^a-z])" replacement=""/&gt;
	///     &lt;tokenizer class="solr.KeywordTokenizerFactory"/&gt;
	///   &lt;/analyzer&gt;
	/// &lt;/fieldType&gt;</pre>
	/// 
	/// @since Solr 3.1
	/// </summary>
	public class PatternReplaceCharFilterFactory : CharFilterFactory
	{
	  private readonly Pattern pattern;
	  private readonly string replacement;
	  private readonly int maxBlockChars;
	  private readonly string blockDelimiters;

	  /// <summary>
	  /// Creates a new PatternReplaceCharFilterFactory </summary>
	  public PatternReplaceCharFilterFactory(IDictionary<string, string> args) : base(args)
	  {
		pattern = getPattern(args, "pattern");
		replacement = get(args, "replacement", "");
		// TODO: warn if you set maxBlockChars or blockDelimiters ?
		maxBlockChars = getInt(args, "maxBlockChars", PatternReplaceCharFilter.DEFAULT_MAX_BLOCK_CHARS);
		blockDelimiters = args.Remove("blockDelimiters");
		if (args.Count > 0)
		{
		  throw new System.ArgumentException("Unknown parameters: " + args);
		}
	  }

	  public override CharFilter create(Reader input)
	  {
		return new PatternReplaceCharFilter(pattern, replacement, maxBlockChars, blockDelimiters, input);
	  }
	}

}