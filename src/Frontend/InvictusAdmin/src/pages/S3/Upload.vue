<template>
    <div class="q-pa-md">
        <div>
            <input type="file" @change="uploadFile" ref="file">
            <button @click="submitFile">Upload!</button>
        </div>
        <div class="q-gutter-sm row items-start">
            <q-uploader url="https://localhost:7215/CloudStorage/UploadFile" color="teal" flat bordered style="max-width: 300px" />
        </div>
    </div>
</template>
<script>
import { uploadFile } from "boot/axios";
export default {
    methods: {
        async submitFile() {
            this.Images = this.$refs.file.files[0];
            console.log(this.Images);
            const response = await uploadFile(this.Images);
        }
    }
}
</script>