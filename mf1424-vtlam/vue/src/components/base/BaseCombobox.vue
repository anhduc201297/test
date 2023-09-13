<template>
    <div class="combobox" :class="inputWidth">
        <BaseInput
            ref="input"
            @onClick="toggleList"
            @keydown.enter="toggleList"
            :modelValue="selectedValue"
            @update:modelValue="filterList"
            :inputLabel="inputLabel"
            :errorMess="errorMess"
            :inputType="inputType"
            :tabIndex="tabIndex"
            :placeHolder="placeHolder"
            :isReadOnly="isReadOnly"
            icon="chevron-down"
            iconSize="icon-16"
        />
        <div v-if="isShowList" :class="['combobox__menu',className]" v-click-outside="hideList">
            <div v-for="(item,index) in filterDataList" 
                :key="index" 
                :class="['combobox__item',{'checked': modelValue == item}]" 
                @click="selectItem(item)" 
            >
                {{item}}
            </div>
        </div>  
    </div> 
</template>

<script>
import BaseInput from "./BaseInput.vue"
import vClickOutside from 'click-outside-vue3'
export default {
    name:"BaseCombobox",
    components:{BaseInput},
    props:["modelValue","selectedItem","className","dataList","errorMess","tabIndex","inputLabel","inputType","inputWidth","labelToolTip","placeHolder","isReadOnly"],
    emits:["selectAction","isError","update:modelValue"],
    computed: {
        /**
         * Lọc danh sách
         * Author: Vũ Tùng Lâm (20/08/2023)
         */
        filterDataList: function(){
            return this.dataList.filter(item => item.toLowerCase().includes(this.searchValue.toLowerCase()));
        }
    },
    methods: {

        /**
         * Ẩn combobox khi click bên ngoài
         * Author: Vũ Tùng Lâm (20/08/2023)
         */
        hideList(){
            if(this.isShowList){
                this.isShowList=false;
            }
            
        },

        /**
         * Ẩn/hiện combobox
         * Author: Vũ Tùng Lâm (20/08/2023)
         */
        toggleList(){
            const me=this;
            me.isShowList=!me.isShowList;
        },
        
        /**
         * trả item được chọn
         * @param {*} item
         * Author:Vũ Tùng Lâm (20/08/2023)
         */
        selectItem(item){
            const me=this;
            me.toggleList();
            me.$refs.input.$el.querySelector("input").focus();
            me.selectedValue=item;
            me.$emit("update:modelValue",item);
            me.$emit("selectAction",item);
            
        },
        
        /**
         * lọc danh sách dữ liệu
         * @param {*} item
         * Author:Vũ Tùng Lâm (20/08/2023)
         */
        filterList(modelValue){
            const me=this;
            me.selectedValue = modelValue;
            me.searchValue = modelValue;
            if(me.isShowList==false){
                me.isShowList=true;
            }
        }
    },
    data() {
        return {
            isShowList: false,
            selectedValue: this.modelValue,
            searchValue:""
        }
    },
    directives: {
      clickOutside: vClickOutside.directive
    },
}
</script>