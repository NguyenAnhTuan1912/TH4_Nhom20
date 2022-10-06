function createInputFileTag() {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = 'image/*';
    input.name = 'ImageFile';
    input.classList.add('input-image');
    input.setAttribute('data-val', true);
    input.setAttribute('data-val-required', 'Ảnh buộc phải có!');
    return input;
}

function createLabelForInputFileTag() {
    const label = document.createElement('label');
    label.textContent = 'Hình ảnh';
    label.htmlFor = 'ImageFile';
    return label;
}

function createValidatationSpan() {
    const span = document.createElement('span');
    span.classList.add('text-danger', 'field-validation-valid');
    span.setAttribute('data-valmsg-for', 'InputFile');
    span.setAttribute('data-valmsg-replace', true);
    return span;
}

function createInputGroup() {
    const div = document.createElement('div');
    const inputFileTag = createInputFileTag();
    const labelForInputFileTag = createLabelForInputFileTag();
    const validatationSpan = createValidatationSpan();
    div.classList.add('form-input__file-main');
    div.append(labelForInputFileTag, inputFileTag, validatationSpan);
    return div;
}

const removeFromInputList = (function () {
    return function(event) {
        const { target } = event;
        target.closest('div.form-input__file').remove();
    }
})();

function createDeleteButtonGroup(amount) {
    const div = document.createElement('div');
    div.classList.add('form-input__file-buttons');
    for(let i = 0; i < amount; i++) {
        const button = document.createElement('button');
        button.classList.add('btn', 'btn-close');
        button.setAttribute('data-button-role', 'delete-input-file');
        button.addEventListener('click', removeFromInputList);
        div.append(button);
    }
    return div;
}

const addCompleteInputFileGroupToList = (function () {
    return function () {
        const inputFileTags = document.getElementById('input-file-tags');
        if (inputFileTags.getElementsByClassName('input-image').length >= 2) return;
        const completeGroup = document.createElement('div');
        completeGroup.classList.add('form-input__file');
        const buttonGroup = createDeleteButtonGroup(1);
        const inputGroup = createInputGroup();
        completeGroup.append(inputGroup, buttonGroup);
        inputFileTags.append(completeGroup);
        amount = inputFileTags.getElementsByClassName('input-image').length;
    }
})();

document.getElementById('js-addNewInputFileTagButton').addEventListener('click', addCompleteInputFileGroupToList);