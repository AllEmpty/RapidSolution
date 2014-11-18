String.prototype.Trim = function () { return this.replace(/(^[ \t\n\r]*)|([ \t\n\r]*$)/g, ''); };
String.prototype.Contains = function (s) { return (this.indexOf(s) > -1); };

function StringBuffer() { this._strings = new Array; };
StringBuffer.prototype.append = function (str) { this._strings.push(str); };
StringBuffer.prototype.toString = function () { return this._strings.join(''); };

function jGet(e) { return document.getElementById(e); };
function Random(n) { return (Math.floor(Math.random() * n)); };

function AjaxRnd() { return new Date().getTime() + '' + Random(10000); };

/********************************/
function parseFloat0(v) {
    v = v || 0;
    v = parseFloat(v);
    if (isNaN(v)) { v = 0; };
    if (v < 0) { v = 0; };
    return v;
};

function parseInt1(v) {
    v = v || 1;
    v = parseInt(v);
    if (isNaN(v)) { v = 1; };
    if (v < 1) { v = 1; };
    return v;
};

function parseInt0(v) {
    v = v || 0;
    v = parseInt(v);
    if (isNaN(v)) { v = 0; };
    if (v < 0) { v = 0; };
    return v;
};



/***********************************************/
function checkbox_ck(the,sName){
	var obj = jGet('lbl_' + sName);
	if(obj){
		if(the.checked){obj.style.color='#ff0000';}else{obj.style.color='#000000';};
	};
};

//checkbox_ck2('isPost');
function checkbox_ck2(s){
	if(!jGet(s)){return;}
	var o = jGet('lbl_' + s);
	if(o){if(jGet(s).checked){o.style.color='#ff0000';}else{o.style.color='#000000';};};
};

//CheckAll('frmList','InfoID',oThe.chkall.checked)
function CheckAll(sFrmName,sChkName,bb){
	var oThe = jGet(sFrmName);
	var oChked = bb;
	var iEl = oThe.elements.length;
	for (var i=0;i<iEl;i++){
		var e = oThe.elements[i];if (e.name == sChkName){e.checked = oChked;};
	};
};

function CheckSelectOne(sFrmName,sChkName){
	var oThe = jGet(sFrmName);
	var iEl = oThe.elements.length;
	var j = 0;
	for (var i=0;i<iEl;i++){
		var e = oThe.elements[i];
		if (e.name==sChkName){if(e.checked){j++;break;};};
	};
	if(j>0){return true;}else{return false;};
};

// only select one
function OnlySelectOne(sFrmName,sChkName,oSel){
	var oThe = jGet(sFrmName);
	var iEl = oThe.elements.length;
	for (var i=0;i<iEl;i++){
		var e = oThe.elements[i];if (e.name == sChkName){e.checked = false;};
	};
	oSel.checked=true;
};

function GetCheckboxOneValue(sFrmName,sChkName){
	if(!jGet(sFrmName)){return "";}
	
	var oThe = jGet(sFrmName);
	var iEl = oThe.elements.length;
	var j = 0;
	var ret = "";
	for (var i=0;i<iEl;i++){
		var e = oThe.elements[i];
		if (e.name==sChkName){if(e.checked){ret = e.value;return ret;};};
	};
	
};

function GetCheckboxArrValue(sFrmName,sChkName){
	if(!jGet(sFrmName)){return "";}
	
	var oThe = jGet(sFrmName);
	var iEl = oThe.elements.length;
	var j = 0;
	var sb = new StringBuffer();
	for (var i=0;i<iEl;i++){
		var e = oThe.elements[i];
		if (e.name==sChkName){if(e.checked){sb.append(e.value + ',');};};
	};
	return sb.toString();
};

function BatDel(sFrmName,sChkName){
	if (!CheckSelectOne(sFrmName,sChkName)){
		alert('没有选择任何记录!');
		return false;
	} else {
		if (confirm('您确认要删除这些信息吗？删除后将不能恢复!!')) {
            return true;
        } else {
            return false;
        };
	};
};

function BatAct(frmID,chkName){
	var act = $('#'+frmID+'_act').val();
	if(act==''){
		alert('请选择操作动作!');
		return false;
	};
	
	var arr =[];    
	  $('#'+frmID+' input[name="'+chkName+'"]:checked').each(function(){    
	   arr.push($(this).val());    
	  });

	if (arr.length<1){
		alert('没有选择任何记录!');
		return false;
	} else {
		$('#'+frmID+'_lst').val(arr.join(','));
		var txt = $('#'+frmID+'_act').find("option:selected").text();
		if (confirm('您确认要 ['+txt+'] 这些信息吗？')) {
            return true;
        } else {
            return false;
        };
	};
	return true;
};


/***********************************************/
function SelectOption(oThe,v){
	if (v!=""){
		var ti=oThe.length;	
		for (var i=0;i<ti;i++){
			if(oThe.options[i].value==v){
				oThe.options.selectedIndex=i;
				oThe.options[i].className="txtred";
				break;
			};
		};
	};
};

/***********************************************/
function jsonToString(obj){
	var THIS = this; 
	switch(typeof(obj)){
		case 'string':
			return '"' + obj.replace(/(["\\])/g, '\\$1') + '"';
		case 'array':
			return '[' + obj.map(THIS.jsonToString).join(',') + ']';
		case 'object':
			 if(obj instanceof Array){
				var strArr = [];
				var len = obj.length;
				for(var i=0; i<len; i++){
					strArr.push(THIS.jsonToString(obj[i]));
				}
				return '[' + strArr.join(',') + ']';
			}else if(obj==null){
				return 'null';

			}else{
				var string = [];
				for (var property in obj) string.push(THIS.jsonToString(property) + ':' + THIS.jsonToString(obj[property]));
				return '{' + string.join(',') + '}';
			}
		case 'number':
			return obj;
		case false:
			return obj;
	}
};