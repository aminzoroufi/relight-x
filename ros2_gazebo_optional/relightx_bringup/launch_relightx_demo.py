"""ReLight-X source file.

Project: ReLight-X
Developer: Amin Zoroufi
Role: AI Researcher / XR Developer
Location: Dubai, UAE
Contact: aminn.zoroufi@gmail.com
Usage: part of the ReLight-X digital twin, adaptive-lighting simulation, board testing, or validation toolchain.
"""

from launch import LaunchDescription
from launch.actions import DeclareLaunchArgument
from launch.substitutions import LaunchConfiguration


def generate_launch_description():
    return LaunchDescription(
        [
            DeclareLaunchArgument(
                "mqtt_bridge_enabled",
                default_value="false",
                description="Placeholder flag for a future ROS2-MQTT bridge.",
            ),
            DeclareLaunchArgument(
                "scenario",
                default_value="normal_car_direction_a",
                description="Scenario name mirrored from the Python backend.",
            ),
        ]
    )
