
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"DispatchableContextExtensions",
            content:"DispatchableContextExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions.Threading/DispatchableContextExtensions',
            title:"DispatchableContextExtensions",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"ReferenceTypeExtensions",
            content:"ReferenceTypeExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/ReferenceTypeExtensions',
            title:"ReferenceTypeExtensions",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"TypeExtensions",
            content:"TypeExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/TypeExtensions',
            title:"TypeExtensions",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"ExpressionExtensions",
            content:"ExpressionExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/ExpressionExtensions',
            title:"ExpressionExtensions",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"SendFuncState",
            content:"SendFuncState",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions.Threading/SendFuncState_1',
            title:"SendFuncState<TResult>",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"TaskExtensions",
            content:"TaskExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/TaskExtensions',
            title:"TaskExtensions",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"ListExtensions",
            content:"ListExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/ListExtensions',
            title:"ListExtensions",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"IDispatchableContext",
            content:"IDispatchableContext",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions.Threading/IDispatchableContext',
            title:"IDispatchableContext",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"EventHandelExtensions",
            content:"EventHandelExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/EventHandelExtensions',
            title:"EventHandelExtensions",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"ValueTypeExtensions",
            content:"ValueTypeExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/ValueTypeExtensions',
            title:"ValueTypeExtensions",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"SynchronizationContextExtensions",
            content:"SynchronizationContextExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions.Threading/SynchronizationContextExtensions',
            title:"SynchronizationContextExtensions",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"EventArgs",
            content:"EventArgs",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/EventArgs_1',
            title:"EventArgs<T>",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"EnumerableExtensions",
            content:"EnumerableExtensions",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions/EnumerableExtensions',
            title:"EnumerableExtensions",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"SendFuncState",
            content:"SendFuncState",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Extensions/api/Anori.Extensions.Threading/SendFuncState_2',
            title:"SendFuncState<TArg1, TResult>",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
