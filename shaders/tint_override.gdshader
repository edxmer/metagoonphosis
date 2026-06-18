shader_type canvas_item;

uniform vec4 override_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

void fragment() {
	vec4 current_color = texture(TEXTURE, UV);
    COLOR = vec4(override_color.rgb, current_color.a);
}
