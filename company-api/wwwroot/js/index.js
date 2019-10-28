$(document).ajaxComplete(function (event, xhr, settings) {
	$("#alertMessage").text("HTTP Status " + xhr.status);
	console.log(JSON.stringify(xhr.responseJSON));
});

$(document).ready(function () {
	getAll();

	$("#btnGet").on("click", function (e) {
		var value = $("#txtGet").val();

		if ($("#optID").prop("checked")) {
			var id = parseInt(value);
			if (Number.isInteger(id)) {
				getById(id);
			} else {
				$("#alertMessage").text("Invalid ID must be an integer");
			}
		} else {
			if (value.length > 1 && /[^0-9]/.test(value.substring(0, 2)) && !(/[^a-zA-Z0-9]/.test(value))) {
				getByIsin(value);
			} else {
				$("#alertMessage").text("Invalid ISIN the first 2 characters must be letters");
			}
		}
	});

	$("#btnAdd").on("click", function (e) {
		$("#btnSave").text("Add");
		$("#id").val(0);
		$("#name").val("");
		$("#exchange").val("");
		$("#ticker").val("");
		$("#isin").val("");
		$("#website").val(null);
		$("#inputDialog").modal("show");
	});

	$("#btnSave").on("click", function (e) {
		$("#inputDialog").modal("hide");

		var item = {
			id: parseInt($("#id").val()),
			name: $("#name").val(),
			exchange: $("#exchange").val(),
			ticker: $("#ticker").val(),
			isin: $("#isin").val()
		};

		var website = $("#website").val();

		if ($.trim(website) !== "") {
			item.website = website;
		}

		if ($("#btnSave").text() == "Add") {
			addItem(item);
		} else {
			updateItem(item);
		}
	});
});

function getAll() {
	$.ajax({
		url: "/api/Company/",
		type: "GET",
		dataType: "json",
		success: function (data) {
			reloadTable(data);
		}
	});
}

function getById(id) {
	$.ajax({
		url: "/api/Company/" + id,
		type: "GET",
		dataType: "json",
		success: function (data) {
			updateInputForm(data);
		}
	});
}

function getByIsin(isin) {
	$.ajax({
		url: "/api/Company/" + isin,
		type: "GET",
		dataType: "json",
		success: function (data) {
			updateInputForm(data);
		}
	});
}

function addItem(item) {
	$.ajax({
		url: "/api/Company/",
		type: "POST",
		accepts: "application/json",
		contentType: "application/json",
		data: JSON.stringify(item),
		success: function (data) {
			getAll();
		}
	});
}

function updateItem(item) {
	$.ajax({
		url: "/api/Company/" + item.id,
		type: "PUT",
		accepts: "application/json",
		contentType: "application/json",
		data: JSON.stringify(item),
		success: function (data) {
			getAll();
		}
	});
}

function reloadTable(items) {
	const tBody = $("#tBody");
	$(tBody).empty();

	$.each(items, function (index, item) {
		const tr = $("<tr></tr>")
			.append($("<td></td>").text(item.id))
			.append($("<td></td>").text(item.name))
			.append($("<td></td>").text(item.exchange))
			.append($("<td></td>").text(item.ticker))
			.append($("<td></td>").text(item.isin))
			.append($("<td></td>").text(item.website))
			.append(
				$("<td></td>").append(
					$("<button type='button' class='btn btn-primary'>Edit</button>").on("click", function () {
						updateInputForm(item);
					})
				)
			);
		tr.appendTo(tBody);
	});
}

function updateInputForm(item) {
	$("#btnSave").text("Update");
	$("#id").val(item.id);
	$("#name").val(item.name);
	$("#exchange").val(item.exchange);
	$("#ticker").val(item.ticker);
	$("#isin").val(item.isin);
	$("#website").val(item.website);
	$("#inputDialog").modal("show");
}